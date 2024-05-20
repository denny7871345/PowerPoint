using System;

namespace PowerPoint
{
    public struct Position
    {
        public Position(int positionX, int positionY)
        {
            X = positionX;
            Y = positionY;
        }

        public int X
        {
            get;
            set;
        }

        public int Y
        {
            get;
            set;
        }

        /// <summary>
        /// to string
        /// </summary>
        /// <returns></returns>
        /*public override string ToString()
        {
            const string FORMAT_STRING = "({0}, {1})";
            return String.Format(FORMAT_STRING, X, Y);
        }*/
    }
}
