using System;
using System.Drawing;

namespace PowerPoint
{
    internal class Ellipse : Shape
    {
        public Ellipse(Position point1, Position point2) : base(point1, point2)
        {
            Name = ELLIPSE_STRING;
        }

        /// <summary>
        /// draw
        /// </summary>
        /// <param name="graphics"></param>
        public override void Draw(Graphics graphics)
        {
            var positionX = Math.Min(_point1.X, _point2.X);
            var positionY = Math.Min(_point1.Y, _point2.Y);
            graphics.DrawEllipse(Pens.Red, positionX, positionY, GetWidth(), GetHeight());

            if (_selected)
            {
                ShowSelectedPreview(graphics);
            }
        }

        private const string ELLIPSE_STRING = "Ellipse";
    }
}

