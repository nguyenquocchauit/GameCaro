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
        #region Properties
        private Timer timerStop;
        private Panel chessBoard;
        private List<Player> players;
        private PictureBox avatar;
        private TextBox playerName;
        private int currentPlayer;
        private List<List<Button>> matrixPositions;
        private Stack<PlayInfo> playTimeLine;
        public Panel ChessBoard { get => chessBoard; set => chessBoard = value; }
        public int CurrentPlayer { get => currentPlayer; set => currentPlayer = value; }
        public TextBox PlayerName { get => playerName; set => playerName = value; }
        public List<List<Button>> MatrixPositions { get => matrixPositions; set => matrixPositions = value; }
        internal List<Player> Players { get => players; set => players = value; }
        internal Stack<PlayInfo> PlayTimeLine { get => playTimeLine; set => playTimeLine = value; }
        public PictureBox Avatar { get => avatar; set => avatar = value; }
        public Timer TimerStop { get => timerStop; set => timerStop = value; }

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
        public GameBoard(Panel board, TextBox PlayerName, PictureBox Avatar, Timer Timer)
        {
            this.TimerStop = Timer;
            this.chessBoard = board;
            this.PlayerName = PlayerName;
            this.Avatar = Avatar;
            this.Players = new List<Player>()
            {
                new Player("Quốc Châu(X)", Image.FromFile(Application.StartupPath + "\\Image\\NQC.jpg"),
                                        Image.FromFile(Application.StartupPath + "\\Image\\X.png")),

                new Player("Hoàn Kim(O)", Image.FromFile(Application.StartupPath + "\\Image\\hoankim.jpg"),
                                   Image.FromFile(Application.StartupPath + "\\Image\\O.png"))
            };
        }
        #endregion
        #region Methods  
        public void DrawGameBoard()
        {
            ChessBoard.Controls.Clear();
            ChessBoard.Enabled = true;
            PlayTimeLine = new Stack<PlayInfo>();
            CurrentPlayer = 0;
            ChangePlayer(); // thiết lập người đánh đầu tiên là QuocChau
            MatrixPositions = new List<List<Button>>();
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

                    chessBoard.Controls.Add(btn);

                    MatrixPositions[i].Add(btn);

                    oldButton = btn;
                }
                oldButton.Location = new Point(0, oldButton.Location.Y + Constant.CellHeight);
                oldButton.Width = 0;
                oldButton.Height = 0;
            }
        }
        private void Btn_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            if (btn.BackgroundImage != null)
                return; // Nếu ô đã được đánh thì ko cho đánh lại
            Mark(btn);
            PlayTimeLine.Push(new PlayInfo(GetChessPoint(btn), CurrentPlayer, btn.BackgroundImage));
            ChangePlayer();
            if (playerMarked != null)
                playerMarked(this, new ButtonClickEvent(GetChessPoint(btn)));
            if (isEndGame(btn))
                EndGame();
        }
        public void OtherPlayerClicked(Point point)
        {
            Button btn = MatrixPositions[point.Y][point.X];
            if (btn.BackgroundImage != null)
                return; // Nếu ô đã được đánh thì ko cho đánh lại
            Mark(btn);
            PlayTimeLine.Push(new PlayInfo(GetChessPoint(btn), CurrentPlayer, btn.BackgroundImage));
            ChangePlayer();
            if (isEndGame(btn))
                EndGame();
        }
        public void EndGame()
        {
            TimerStop.Stop();
            MessageBox.Show(Players[CurrentPlayer == 1 ? 0 : 1].Name + " chiến thắng");
            ChessBoard.Enabled = false;
            DrawGameBoard();
        }
        private bool isEndGame(Button btn)
        {
            return isEndRow(btn) || isEndCol(btn) || isEndAuxiliaryDiagonal(btn) || isEndMainDiagonal(btn);
        }
        private void ChangePlayer()
        {
            PlayerName.Text = Players[CurrentPlayer].Name;
            Avatar.Image = Players[CurrentPlayer].Avatar;
        }
        private void Mark(Button btn)
        {
            btn.BackgroundImage = Players[CurrentPlayer].Symbol;
            CurrentPlayer = CurrentPlayer == 1 ? 0 : 1;
        }
        public bool Undo()
        {
            if (PlayTimeLine.Count <= 0)
            {
                return false;
            }
            PlayInfo oldPoint = PlayTimeLine.Peek();
            
            return UndoAsStep() && UndoAsStep();
        }

        private bool UndoAsStep()
        {


            PlayInfo oldPoint = PlayTimeLine.Pop();
            Button btn = MatrixPositions[oldPoint.Point.Y][oldPoint.Point.X];

            btn.BackgroundImage = null;


            if (PlayTimeLine.Count <= 0)
            {
                CurrentPlayer = 0;
            }
            else
            {
                oldPoint = PlayTimeLine.Peek();
            }

            ChangePlayer();

            return true;
        }
        private Point GetChessPoint(Button btn)
        {
            int vertical = Convert.ToInt32(btn.Tag);
            int horizontal = MatrixPositions[vertical].IndexOf(btn);
            Point point = new Point(horizontal, vertical);
            return point;
        }
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
            
            return countLeft + countRight == 5;
        }
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

            return countTop + countBottom == 5;
        }
        private bool isEndMainDiagonal(Button btn)
        {
            Point point = GetChessPoint(btn);

            int countTop = 0;
            for (int i = 0; i <= point.X; i++)
            {
                if (point.X - i < 0 || point.Y - i < 0)
                    break;

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
                if (point.Y + i >= Constant.CellHeight || point.X + i >= Constant.CellWidth)
                    break;

                if (MatrixPositions[point.Y + i][point.X + i].BackgroundImage == btn.BackgroundImage)
                {
                    countBottom++;
                }
                else
                    break;
            }

            return countTop + countBottom == 5;
        }
        private bool isEndAuxiliaryDiagonal(Button btn)
        {
            Point point = GetChessPoint(btn);

            int countTop = 0;
            for (int i = 0; i <= point.X; i++)
            {
                if (point.X + i > Constant.CellWidth || point.Y - i < 0)
                    break;

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
                if (point.Y + i >= Constant.CellHeight || point.X - i < 0)
                    break;

                if (MatrixPositions[point.Y + i][point.X - i].BackgroundImage == btn.BackgroundImage)
                {
                    countBottom++;
                }
                else
                    break;
            }

            return countTop + countBottom == 5;
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
