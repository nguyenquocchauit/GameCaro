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

            chessBoardManager = new GameBoard(pn_GameBoard, txt_PlayerName, pb_Avatar, tmCoolDown);

           
            chessBoardManager.PlayerMarked += ChessBoard_PlayerMarked;

            prcbCoolDown.Step = Constant.COOL_DOWN_STEP;
            prcbCoolDown.Maximum = Constant.COOL_DOWN_TIME;
            prcbCoolDownSezo();

            tmCoolDown.Interval = Constant.COOL_DOWN_INTERVAL;

            NewGame();

            socketManager = new SocketManager();

        }
        #region Connect LAN
        // Kết nối LAN bằng nút bấm 
        private void btn_LAN_Click(object sender, EventArgs e)
        {
            // Form nào click trước sẽ là Server và ngược lại là Client
            socketManager.IP = txt_IP.Text;
            if (socketManager.ConnectServer() == false)
            {
                socketManager.isServer = true;
                pn_GameBoard.Enabled = true;
                socketManager.CreateServer();
            }
            else
            {   
                socketManager.isServer = false;
                pn_GameBoard.Enabled = false;
                Listen();
            }

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
        // Xử lý data khi được send
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
                        NewGame();
                        pn_GameBoard.Enabled = false;
                    }));
                    break;
                case (int)SocketCommand.SEND_POINT:
                    this.Invoke((MethodInvoker)(() => {
                        prcbCoolDownSezo();
                        pn_GameBoard.Enabled = true;
                        tmCoolDown.Start();
                        chessBoardManager.OtherPlayerClicked(data.Point);
                       
                    }));
                    break;
                case (int)SocketCommand.UNDO:
                    chessBoardManager.Undo();
                    prcbCoolDownSezo();
                    break;
                case (int)SocketCommand.END_GAME:
                    tmCoolDown.Stop();
                    MessageBox.Show(data.Message + " Thắng cuộc");
                    EndGame();
                    break;
                case (int)SocketCommand.TIME_OUT:
                    string name = chessBoardManager.Players[chessBoardManager.CurrentPlayer == 1 ? 0 : 1].Name;
                    tmCoolDown.Stop();
                    MessageBox.Show(name + " Thắng cuộc");
                    EndGame();
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
        #endregion

        void ChessBoard_EndedGame(object sender, EventArgs e)
        {
            EndGame();
            socketManager.Send(new SocketData((int)SocketCommand.END_GAME, "", new Point()));
        }
        private void tmCoolDown_Tick(object sender, EventArgs e)
        {

            prcbCoolDown.PerformStep();

            if (prcbCoolDown.Value >= prcbCoolDown.Maximum)
            {
                EndGame();
                socketManager.Send(new SocketData((int)SocketCommand.TIME_OUT, "", new Point()));
            }
        }
        void EndGame()
        {
            tmCoolDown.Stop();
            pn_GameBoard.Enabled = false;
            MessageBox.Show("Kết thúc game");


        }
        void NewGame()
        {

            prcbCoolDownSezo();
            tmCoolDown.Stop();
            chessBoardManager.DrawGameBoard();

        }
        public void Undo()
        {

            prcbCoolDownSezo();
            chessBoardManager.Undo();
            socketManager.Send(new SocketData((int)SocketCommand.UNDO, "", new Point()));
        }
        void Quit()
        {
            if (MessageBox.Show("Bạn có chắc muốn thoát chương trình ?", "Thông báo", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
                Application.Exit();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc muốn thoát chương trình ?", "Thông báo", MessageBoxButtons.OKCancel) != System.Windows.Forms.DialogResult.OK)
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
        // thiết lập form khi được mở lên là true
        private void Form1_Load(object sender, EventArgs e)
        {
            this.KeyPreview = true;

        }
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control == true && e.KeyCode == Keys.Q)
            {
                Quit();
            }
            if (e.Control == true && e.KeyCode == Keys.N)
            {
                NewGame();
            }
            if (e.Control == true && e.KeyCode == Keys.Z)
            {
                Undo();
            }
        }
        private void btn_Undo_Click(object sender, EventArgs e)
        {
            Undo();
            
        }

        private void btnNewGame_Click(object sender, EventArgs e)
        {
            NewGame();
            socketManager.Send(new SocketData((int)SocketCommand.NEW_GAME, "", new Point()));
            pn_GameBoard.Enabled = true;
        }

        private void btnQuit_Click(object sender, EventArgs e)
        {
            Quit();
        }


        
        private void ChessBoard_PlayerMarked(object sender, ButtonClickEvent e)
        {
            tmCoolDown.Start();
            pn_GameBoard.Enabled = false;
            prcbCoolDownSezo();
            socketManager.Send(new SocketData((int)SocketCommand.SEND_POINT, "", e.ClickedPoint));

            Listen();
        }
        public void prcbCoolDownSezo()
        {
            prcbCoolDown.Value = 0;
        }
    }
}
