using System.Drawing;

namespace PowerPoint
{
    internal class Line : Shape
    {
        public Line(Position point1, Position point2) : base(point1, point2)
        {
            this.Name = LINE_STRING;
        }

        /// <summary>
        /// draw
        /// </summary>
        /// <param name="graphics"></param>
        public override void Draw(Graphics graphics)
        {
            graphics.DrawLine(Pens.Blue, _point1.X, _point1.Y, _point2.X, _point2.Y);

            if (_selected)
            {
                ShowSelectedPreview(graphics);
            }
                
        }

        private const string LINE_STRING = "Line";
    }
}
