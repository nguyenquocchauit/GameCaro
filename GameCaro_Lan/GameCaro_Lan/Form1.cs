using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameCaro_Lan
{
    public partial class Form1 : Form
    {
        #region Porperties
        SocketManager socketManager;
        GameBoard chessBoardManager;
        #endregion
        public Form1()
        {
            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false;
            chessBoardManager = new GameBoard(pn_GameBoard, txt_PlayerName, pb_Avatar, tmCoolDown, prcbCoolDown);
            socketManager = new SocketManager();
            chessBoardManager.PlayerMarked += ChessBoard_PlayerMarked;
            prcbCoolDown.Step = Constant.COOL_DOWN_STEP;
            prcbCoolDown.Maximum = Constant.COOL_DOWN_TIME;
            chessBoardManager.prcbCoolDownZero();
            tmCoolDown.Interval = Constant.COOL_DOWN_INTERVAL;
            chessBoardManager.NewGame();
        }
        #region Connect LAN
        // Kết nối LAN bằng nút bấm. Sau khi kết nối sẽ đóng chức năng kết nối LAN lần nữa tránh bị lỗi (giải pháp tạm thời)
        private void btn_LAN_Click(object sender, EventArgs e)
        {
            // Form nào click trước sẽ là Server và ngược lại là Client
            socketManager.IP = txt_IP.Text;
            if (socketManager.ConnectServer() == false)
            {
                socketManager.isServer = true;
                pn_GameBoard.Enabled = true;
                socketManager.CreateServer();
                btn_LAN.Enabled = false;
            }
            else
            {   
                socketManager.isServer = false;
                pn_GameBoard.Enabled = false;
                MessageBox.Show("Kết nối thành công!");
                btn_LAN.Enabled = false;
                Listen();
            }

        }
        // hàm lắng nghe để trao đổi gói tin qua lại khi thực hiện các thao tác trên form
        private void Listen()
        {
            try
            {
                Thread listernThread = new Thread(() =>
                {
                    while (true)
                    {
                        try
                        {
                            SocketData data = (SocketData)socketManager.Receive();
                            ProcessData(data);
                            break;
                        }
                        catch (Exception e)
                        {
                        }
                        Thread.Sleep(10);
                    }
                });
                listernThread.IsBackground = true;
                listernThread.Start();
            }
            catch
            { 
            }
        }
        // Xử lý dât khi được send
        private void ProcessData(SocketData data)
        {
            switch (data.Command)
            {
                case (int)SocketCommand.NOTIFY:
                    MessageBox.Show(data.Message);
                    break;
                case (int)SocketCommand.NEW_GAME:
                    this.Invoke((MethodInvoker)(() =>
                    {
                        chessBoardManager.NewGame();
                        pn_GameBoard.Enabled = false;
                        btn_Undo.Enabled = false; // đóng chức năng đánh lại khi chưa được đánh lượt nào tránh lỗi game
                    }));
                    break;
                case (int)SocketCommand.SEND_POINT:
                    this.Invoke((MethodInvoker)(() => {
                        chessBoardManager.prcbCoolDownZero();
                        pn_GameBoard.Enabled = true;
                        btn_Undo.Enabled = true;
                        tmCoolDown.Start();
                        chessBoardManager.OtherPlayerClicked(data.Point); // gửi lượt đánh
                    }));
                    break;
                case (int)SocketCommand.UNDO:
                    chessBoardManager.Undo();
                    chessBoardManager.prcbCoolDownZero();
                    break;
                case (int)SocketCommand.TIME_OUT:
                    string name = chessBoardManager.Players[chessBoardManager.CurrentPlayer == 1 ? 0 : 1].Name;
                    MessageBox.Show(name + " Thắng cuộc");
                    chessBoardManager.EndGame();
                    break;
                case (int)SocketCommand.QUIT:
                    tmCoolDown.Stop();
                    MessageBox.Show("Người chơi đã thoát");
                    break;
                default:
                    break;
            }
            Listen();
        }
        // hàm xử lý gửi lượt đánh và lắng nghe người chơi còn lại thực hiện hành động đánh 
        void ChessBoard_PlayerMarked(object sender, ButtonClickEvent e)
        {
            tmCoolDown.Start();
            prcbCoolDown.Value = 0;
            tmCoolDown.Start();
            pn_GameBoard.Enabled = false;
            btn_Undo.Enabled = false;
            chessBoardManager.prcbCoolDownZero();
            socketManager.Send(new SocketData((int)SocketCommand.SEND_POINT, "", e.ClickedPoint));
            Listen();
        }
        #endregion
        #region xử lý sự kiện game(thời gian, đánh lại, thoát game và button form)
        // đếm thời gian lượt của người chơi. vượt quá thời gian cho phép sẽ kết thúc game 2 form
        private void tmCoolDown_Tick(object sender, EventArgs e)
        {
            prcbCoolDown.PerformStep();
            if (prcbCoolDown.Value >= prcbCoolDown.Maximum)
            {
                chessBoardManager.EndGame();
                socketManager.Send(new SocketData((int)SocketCommand.TIME_OUT, "", new Point()));
            }
        }
        // đánh lại nước đánh(người thực hiện đánh lại) và gửi commnand tới form người chơi
        public void Undo()
        {
            chessBoardManager.prcbCoolDownZero();
            chessBoardManager.Undo();
            socketManager.Send(new SocketData((int)SocketCommand.UNDO, "", new Point()));
        }
        // thoát game cơ bản của button Quit
        public void Quit()
        {
            if (MessageBox.Show("Bạn có chắc muốn thoát chương trình ?", "Thông báo", MessageBoxButtons.OKCancel) 
                == System.Windows.Forms.DialogResult.OK)
                Application.Exit();
        }
        // xử lý form đóng sẽ thoát game và gửi commnand tới form người chơi
        void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc muốn thoát chương trình ?", "Thông báo", MessageBoxButtons.OKCancel) 
                != System.Windows.Forms.DialogResult.OK)
                e.Cancel = true;
            else
            {
                try
                {
                    socketManager.Send(new SocketData((int)SocketCommand.QUIT, "", new Point()));
                }
                catch
                {
                }
            }
        }
        // nút bấm đánh lại
        private void btn_Undo_Click(object sender, EventArgs e)
        {
            Undo();
        }
        // nút bấm làm mới game và gửi commnand tới form người chơi
        private void btnNewGame_Click(object sender, EventArgs e)
        {
            chessBoardManager.NewGame();
            socketManager.Send(new SocketData((int)SocketCommand.NEW_GAME, "", new Point()));
            pn_GameBoard.Enabled = true;
        }
        // nút bấm thoát game
        private void btnQuit_Click(object sender, EventArgs e)
        {
            Quit();
        }
        #endregion
        #region Thiết lập form và phím tắt
        // thiết lập form khi được mở lên là true
        private void Form1_Load(object sender, EventArgs e)
        {
            this.KeyPreview = true;
            btn_Undo.Enabled = false; // form khi được mở lên sẽ không có chức năng đánh lại (vì chưa được đánh)
        }
        // Form shown sẽ hiện IPV4 để dùng kết nối LAN
        private void Form1_Shown(object sender, EventArgs e)
        {
            txt_IP.Text = socketManager.GetLocalIPv4(NetworkInterfaceType.Wireless80211);
            if (string.IsNullOrEmpty(txt_IP.Text))
            {
                txt_IP.Text = socketManager.GetLocalIPv4(NetworkInterfaceType.Ethernet);
            }
        }
        // Thiết lập phím tắt cho form
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control == true && e.KeyCode == Keys.Q)
            {
                Quit();
            }
            if (e.Control == true && e.KeyCode == Keys.N)
            {
                socketManager.Send(new SocketData((int)SocketCommand.NEW_GAME, "", new Point()));
                chessBoardManager.NewGame();
                pn_GameBoard.Enabled = true;
            }
            if (e.Control == true && e.KeyCode == Keys.Z)
            {
                Undo();
            }
        }
        #endregion

    }
}
