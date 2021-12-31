using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameCaro_Lan
{
    public class GameBoard
    {
        #region Thuộc tính cần dùng
        private Timer timerStop;
        private Panel chessBoard;
        private PictureBox avatar;
        private TextBox playerName;
        private int currentPlayer;
        private ProgressBar prcbCoolDown;
        private List<List<Button>> matrixPositions;
        private Stack<PlayInfo> playTimeLine;
        private List<Player> players;
        public Panel ChessBoard { get => chessBoard; set => chessBoard = value; }
        public int CurrentPlayer { get => currentPlayer; set => currentPlayer = value; }
        public TextBox PlayerName { get => playerName; set => playerName = value; }
        public List<List<Button>> MatrixPositions { get => matrixPositions; set => matrixPositions = value; }
        internal List<Player> Players { get => players; set => players = value; }
        internal Stack<PlayInfo> PlayTimeLine { get => playTimeLine; set => playTimeLine = value; }
        public PictureBox Avatar { get => avatar; set => avatar = value; }
        public Timer TimerStop { get => timerStop; set => timerStop = value; }
        public ProgressBar PrcbCoolDown { get => prcbCoolDown; set => prcbCoolDown = value; }
        private event EventHandler<ButtonClickEvent> playerMarked;
        public event EventHandler<ButtonClickEvent> PlayerMarked
        {
            add
            {
                playerMarked += value;
            }
            remove
            {
                playerMarked -= value;
            }
        }
        #endregion
        #region Initialize
        public GameBoard(Panel board, TextBox PlayerName, PictureBox Avatar, Timer Timer, ProgressBar prcbCoolDown)
        {
            this.TimerStop = Timer;
            this.chessBoard = board;
            this.PlayerName = PlayerName;
            this.Avatar = Avatar;
            this.PrcbCoolDown = prcbCoolDown;
            this.Players = new List<Player>()
            {
                // thiết lập tên, hình ảnh và kí tự người chơi
                new Player("Quốc Châu(X)", Image.FromFile(Application.StartupPath + "\\Image\\NQC.jpg"),
                                        Image.FromFile(Application.StartupPath + "\\Image\\X.png")),

                new Player("Hoàn Kim(O)", Image.FromFile(Application.StartupPath + "\\Image\\hoankim.jpg"),
                                   Image.FromFile(Application.StartupPath + "\\Image\\O.png"))
            };
        }
        #endregion
        #region xử lý game  
        // hàm vẽ bàn cờ
        public void DrawGameBoard()
        {
            ChessBoard.Controls.Clear(); // làm mới form mỗi lần newgame
            ChessBoard.Enabled = true;
            PlayTimeLine = new Stack<PlayInfo>();
            MatrixPositions = new List<List<Button>>();
            CurrentPlayer = 0;
            ChangePlayer(); // thiết lập người đánh đầu tiên là QuocChau
            Button oldButton = new Button() { Width = 0, Location = new Point(0, 0) };
            for (int i = 0; i < Constant.CHESS_BOARD_WIDTH; i++)
            {
                MatrixPositions.Add(new List<Button>());

                for (int j = 0; j < Constant.CHESS_BOARD_WIDTH; j++)
                {
                    Button btn = new Button()
                    {
                        Width = Constant.CellWidth,
                        Height = Constant.CellHeight,
                        Location = new Point(oldButton.Location.X + oldButton.Width, oldButton.Location.Y),
                        BackgroundImageLayout = ImageLayout.Stretch,
                        Tag = i.ToString()
                    };
                    btn.Click += Btn_Click;
                    chessBoard.Controls.Add(btn); // in ô đánh ra form
                    MatrixPositions[i].Add(btn); // lưu ô đánh thành mảng
                    oldButton = btn;
                }
                // thiết lập oldbutton có tọa độ được cập nhật xuống một dòng
                oldButton.Location = new Point(0, oldButton.Location.Y + Constant.CellHeight);
                oldButton.Width = 0;
                oldButton.Height = 0;
            }
        }
        // hàm in tên và thay đổi kí tự người chơi thông qua thuộc tính CurrentPlayer (0/1)
        private void ChangePlayer()
        {
            PlayerName.Text = Players[CurrentPlayer].Name;
            Avatar.Image = Players[CurrentPlayer].Avatar;
        }
        // hàm in hình ảnh ký tự ra bàn cờ thông qua thuộc tính CurrentPlayer
        private void Symbol(Button btn)
        {
            btn.BackgroundImage = Players[CurrentPlayer].Symbol;
            CurrentPlayer = CurrentPlayer == 1 ? 0 : 1;
        }
        // hàm xử xử lý đánh ô đánh trên bàn cờ
        private void Btn_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            if (btn.BackgroundImage != null)
                return; // Nếu ô đã được đánh thì ko cho đánh lại
            Symbol(btn);
            PlayTimeLine.Push(new PlayInfo(GetChessPoint(btn), CurrentPlayer, btn.BackgroundImage));
            ChangePlayer();

            if (playerMarked != null)
                playerMarked(this, new ButtonClickEvent(GetChessPoint(btn)));
            if (isEndGame(btn))
                EndGame();
        }
        // hàm xử xử lý đánh ô đánh trên bàn cờ dành cho 2 người chơi
        public void OtherPlayerClicked(Point point)
        {
            Button btn = MatrixPositions[point.Y][point.X];
            if (btn.BackgroundImage != null)
                return; // Nếu ô đã được đánh thì ko cho đánh lại
            Symbol(btn);
            PlayTimeLine.Push(new PlayInfo(GetChessPoint(btn), CurrentPlayer, btn.BackgroundImage));
            ChangePlayer();
            if (isEndGame(btn))
                EndGame();
        }
        // hàm kết thúc game và vẽ lại bàn cờ
        public void EndGame()
        {
            TimerStop.Stop();
            MessageBox.Show(Players[CurrentPlayer == 1 ? 0 : 1].Name + " chiến thắng");
            ChessBoard.Enabled = false;
            DrawGameBoard();
        }
        // hàm thiết lập lại thanh progressbar về bằng 0 tiện dùng lại nhiều lần
        public void prcbCoolDownZero()
        {
            PrcbCoolDown.Value = 0;
        }
        // hàm làm mới game
        public void NewGame()
        {
            prcbCoolDownZero();
            timerStop.Stop();
            DrawGameBoard();

        }
        // hàm kết thúc game nếu thỏa các trường hợp kết thúc game
        private bool isEndGame(Button btn)
        {
            return isEndRow(btn) || isEndCol(btn) || isEndAuxiliaryDiagonal(btn) || isEndMainDiagonal(btn);
        }
        // hàm lấy tọa độ button đã được đánh
        private Point GetChessPoint(Button btn)
        {
            int vertical = Convert.ToInt32(btn.Tag);
            int horizontal = MatrixPositions[vertical].IndexOf(btn);
            Point point = new Point(horizontal, vertical);
            return point;
        }
        // xét thắng theo hàng ngang
        private bool isEndRow(Button btn)
        {
            Point point = GetChessPoint(btn);

            int countLeft = 0;
            for (int i = point.X; i >= 0; i--)
            {
                if (MatrixPositions[point.Y][i].BackgroundImage == btn.BackgroundImage)
                {
                    countLeft++;
                }
                else
                    break;
            }

            int countRight = 0;
            for (int i = point.X + 1; i < Constant.CellWidth; i++)
            {
                if (MatrixPositions[point.Y][i].BackgroundImage == btn.BackgroundImage)
                {
                    countRight++;
                }
                else
                    break;
            }
            
            return countLeft + countRight >= 5;
        }
        // xét thắng theo dọc
        private bool isEndCol(Button btn)
        {
            Point point = GetChessPoint(btn);

            int countTop = 0;
            for (int i = point.Y; i >= 0; i--)
            {
                if (MatrixPositions[i][point.X].BackgroundImage == btn.BackgroundImage)
                {
                    countTop++;
                }
                else
                    break;
            }

            int countBottom = 0;
            for (int i = point.Y + 1; i < Constant.CellHeight; i++)
            {
                if (MatrixPositions[i][point.X].BackgroundImage == btn.BackgroundImage)
                {
                    countBottom++;
                }
                else
                    break;
            }

            return countTop + countBottom >= 5;
        }
        // xét thắng theo đường chéo chính
        private bool isEndMainDiagonal(Button btn)
        {
            Point point = GetChessPoint(btn);

            int countTop = 0;
            for (int i = 0; i <= point.X; i++)
            {
                //if (point.X - i < 0 || point.Y - i < 0)
                //    break;

                if (MatrixPositions[point.Y - i][point.X - i].BackgroundImage == btn.BackgroundImage)
                {
                    countTop++;
                }
                else
                    break;
            }

            int countBottom = 0;
            for (int i = 1; i <= Constant.CellWidth - point.X; i++)
            {
                //if (point.Y + i >= Constant.CellHeight || point.X + i >= Constant.CellWidth)
                //    break;

                if (MatrixPositions[point.Y + i][point.X + i].BackgroundImage == btn.BackgroundImage)
                {
                    countBottom++;
                }
                else
                    break;
            }

            return countTop + countBottom >= 5;
        }
        // xét thắng theo đường chép phụ
        private bool isEndAuxiliaryDiagonal(Button btn)
        {
            Point point = GetChessPoint(btn);

            int countTop = 0;
            for (int i = 0; i <= point.X; i++)
            {
                //if (point.X + i > Constant.CellWidth || point.Y - i < 0)
                //    break;

                if (MatrixPositions[point.Y - i][point.X + i].BackgroundImage == btn.BackgroundImage)
                {
                    countTop++;
                }
                else
                    break;
            }

            int countBottom = 0;
            for (int i = 1; i <= Constant.CellWidth - point.X; i++)
            {
                //if (point.Y + i >= Constant.CellHeight || point.X - i < 0)
                //    break;

                if (MatrixPositions[point.Y + i][point.X - i].BackgroundImage == btn.BackgroundImage)
                {
                    countBottom++;
                }
                else
                    break;
            }

            return countTop + countBottom >= 5;
        }
        // hàm đánh lại. Quay lại 2 nước để dễ tính trạng thái CurrentPlayer đối với form đánh hai người . Nếu một người thì không cần
        public bool Undo()
        {
            //if (PlayTimeLine.Count <= 0)
            //{
            //    return false;
            //}
            //PlayInfo oldPoint = PlayTimeLine.Peek();
            return UndoAsStep() && UndoAsStep();
        }
        // hàm lấy đánh lại
        private bool UndoAsStep()
        {
            PlayInfo oldPoint = PlayTimeLine.Pop();
            Button btn = MatrixPositions[oldPoint.Point.Y][oldPoint.Point.X];
            btn.BackgroundImage = null;
            //if (PlayTimeLine.Count <= 0)
            //{
            //    CurrentPlayer = 0;
            //}
            //else
            //{
            //    oldPoint = PlayTimeLine.Peek();
            //}
            ChangePlayer();
            return true;
        }
        #endregion
    }
    
    public class ButtonClickEvent : EventArgs
    {
        private Point clickedPoint;
        public Point ClickedPoint { get => clickedPoint; set => clickedPoint = value; }
        public ButtonClickEvent(Point point)
        {
            this.ClickedPoint = point;
        }
    }
    
}
