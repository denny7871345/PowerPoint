using System;
using System.Drawing;

namespace PowerPoint
{
    internal class Rectangle : Shape
    {
        public Rectangle(Position point1, Position point2) : base(point1, point2)
        {
            Name = RECTANGLE_STRING;
        }

        /// <summary>
        /// draw
        /// </summary>
        /// <param name="graphics"></param>
        public override void Draw(Graphics graphics)
        {
            var positionX = Math.Min(this._point1.X, this._point2.X);
            var positionY = Math.Min(this._point1.Y, this._point2.Y);
            graphics.DrawRectangle(Pens.Pink, positionX, positionY, this.GetWidth(), this.GetHeight());

            if (_selected)
            {
                ShowSelectedPreview(graphics);
            }
                
        }
        private const string RECTANGLE_STRING = "Rectangle";
    }
}
