using System;
using System.Drawing;

namespace PowerPoint
{
    public abstract class Shape
    {
        /// <summary>
        /// shape
        /// </summary>
        protected Position _point1;
        protected Position _point2;
        protected Shape(Position point1, Position point2)
        {
            _point1 = point1;
            _point2 = point2;
        }

        /// <summary>
        /// GetPoint1
        /// </summary>
        public Position GetPoint1()
        {
            return _point1;
        }

        /// <summary>
        /// GetPoint2
        /// </summary>
        public Position GetPoint2()
        {
            return _point2;
        }

        /// <summary>
        /// SetPoint1
        /// </summary>
        public void SetPoint1(Position position)
        {
            _point1.X = position.X;
            _point1.Y = position.Y;
        }

        /// <summary>
        /// SetPoint2
        /// </summary>
        public void SetPoint2(Position position)
        {
            _point2.X = position.X;
            _point2.Y = position.Y;
        }

        const int TWO = 2;

        /// <summary>
        /// show
        /// </summary>
        protected virtual void ShowSelectedPreview(Graphics graphics)
        {
            var positionX = Math.Min(_point1.X, _point2.X);
            var positionY = Math.Min(_point1.Y, _point2.Y);
            graphics.DrawRectangle(Pens.Red, positionX, positionY, GetWidth(), GetHeight());

            const int RADIUS = 10;
            DrawEllipseByCenterAndRadius(graphics, _point1, RADIUS);
            DrawEllipseByCenterAndRadius(graphics, new Position(_point1.X, (_point1.Y + _point2.Y) / TWO), RADIUS);
            DrawEllipseByCenterAndRadius(graphics, new Position(_point1.X, _point2.Y), RADIUS);
            DrawEllipseByCenterAndRadius(graphics, new Position((_point1.X + _point2.X) / TWO, _point2.Y), RADIUS);
            DrawEllipseByCenterAndRadius(graphics, _point2, RADIUS);
            DrawEllipseByCenterAndRadius(graphics, new Position(_point2.X, (_point1.Y + _point2.Y) / TWO), RADIUS);
            DrawEllipseByCenterAndRadius(graphics, new Position(_point2.X, _point1.Y), RADIUS);
            DrawEllipseByCenterAndRadius(graphics, new Position((_point1.X + _point2.X) / TWO, _point1.Y), RADIUS);
        }

        /// <summary>
        /// Get Width
        /// </summary>
        protected int GetWidth()
        {
            return Math.Abs(this.GetPoint1().X - this.GetPoint2().X);
        }

        /// <summary>
        /// Get Height
        /// </summary>
        protected int GetHeight()
        {
            return Math.Abs(this.GetPoint1().Y - this.GetPoint2().Y);
        }

        /// <summary>
        /// ellipse
        /// </summary>
        /// <param name="graphics"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="radius"></param>
        private void DrawEllipseByCenterAndRadius(Graphics graphics, Position point, float radius)
        {
            graphics.DrawEllipse(
                Pens.Black,
                point.X - (radius / TWO),
                point.Y - (radius / TWO),
                radius,
                radius
            );
        }

        /// <summary>
        /// draw
        /// </summary>
        /// <param name="graphics"></param>
        public abstract void Draw(Graphics graphics);
        /*private Position point1;
        private Position point2;*/
        public string Info
        {
            get
            {
                const string FORMAT_STRING = "{0}, {1}";
                return String.Format(FORMAT_STRING, _point1, _point2);
            }
        }

        public string Name
        {
            get;
            protected set;
        }
        /*public Position _point1 { get => point1; set => point1 = value; }
        public Position _point2 { get => point2; set => point2 = value; }*/

        protected bool _selected;

        /// <summary>
        /// draw
        /// </summary>
        public bool IsSelected()
        {
            return _selected;
        }

        /// <summary>
        /// draw
        /// </summary>
        public void SetSelected(bool set)
        {
            _selected = set;
        }

    }
}
