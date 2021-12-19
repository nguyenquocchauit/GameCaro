using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameCaro_Lan
{
    class PlayInfo
    {
        private Point point;
        private int currentPlayer;
        private Image symbol;

        public Point Point { get => point; set => point = value; }
        public int CurrentPlayer { get => currentPlayer; set => currentPlayer = value; }
        public Image Symbol { get => symbol; set => symbol = value; }
        public PlayInfo(Point point, int currentPlayer, Image symbol)
        {
            this.Point = point;
            this.CurrentPlayer = currentPlayer;
            this.Symbol = symbol;
        }
    }
}
