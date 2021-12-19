using System;
using System.Drawing;

namespace GameCaro_Lan
{
    [Serializable]
    public class SocketData
    {
        private int command;
        private Point point;
        private string message;

        public int Command
        {
            get { return command; }
            set { command = value; }
        }

        public Point Point
        {
            get { return point; }
            set { point = value; }
        }

        public string Message
        {
            get { return message; }
            set { message = value; }
        }

        public SocketData(int command, string message, Point point)
        {
            this.Command = command;
            this.Point = point;
            this.Message = message;
        }
    }

    public enum SocketCommand
    {
        SEND_POINT,
        NOTIFY,
        NEW_GAME,
        UNDO,
        END_GAME,
        TIME_OUT,
        QUIT
    }
}
