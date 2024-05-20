using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace PowerPoint
{
    public class ViewModel
    {
        public event ModelChangedEventHandler ModelChanged;
        public delegate void ModelChangedEventHandler();

        public ViewModel()
        {
            Shapes = new BindingList<Shape>();
        }

        /// <summary>
        /// draw
        /// </summary>
        /// <param name="graphics"></param>
        public void Draw(Graphics graphics)
        {
            foreach (var shape in Shapes)
            {
                shape.Draw(graphics);
            }

            if (!_isPressed)
                return;

            if (_previewShape != null)
                _previewShape.Draw(graphics);
        }

        /// <summary>
        /// add
        /// </summary>
        /// <param name="shape"></param>
        public void Add(Shape shape)
        {
            Shapes.Add(shape);
            NotifyModelChanged();
        }

        /// <summary>
        /// remove at
        /// </summary>
        /// <param name="index"></param>
        public void RemoveAt(int index)
        {
            Shapes.RemoveAt(index);
            NotifyModelChanged();
        }

        /// <summary>
        /// notify model changed
        /// </summary>
        private void NotifyModelChanged()
        {
            if (ModelChanged != null)
            {
                ModelChanged.Invoke();
            }
        }

        /// <summary>
        /// delete
        /// </summary>
        public void DeleteSelected()
        {
            var index = Shapes.ToList().FindIndex(s => s.IsSelected());
            Shapes.RemoveAt(index);
            NotifyModelChanged();
        }

        /// <summary>
        /// range
        /// </summary>
        /// <param name="point1"></param>
        /// <param name="point2"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        private bool IsInRange(Position point1, Position point2, Position target)
        {
            var xMin = Math.Min(point1.X, point2.X);
            var xMax = Math.Max(point1.X, point2.X);
            var yMin = Math.Min(point1.Y, point2.Y);
            var yMax = Math.Max(point1.Y, point2.Y);

            return xMin < target.X && target.X < xMax && yMin < target.Y && target.Y < yMax;
        }

        /// <summary>
        /// pressed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void HandleCanvasPressed(object sender, MouseEventArgs e)
        {
            _isPressed = true;
            _previousMousePosition = new Position(e.X, e.Y);

            if (this.GetCurrentMode() == Mode.Select)
            {
                foreach (var s in Shapes)
                {
                    s.SetSelected(IsInRange(s.GetPoint1(), s.GetPoint2(), _previousMousePosition));
                    _isShape |= s.IsSelected();
                }

                NotifyModelChanged();
                return;
            }

            _previewShape = ShapeFactory.CreateShape
            (
                SelectedShape,
                new Position(e.X, e.Y),
                new Position(e.X, e.Y)
            );
        }

        /// <summary>
        /// moved
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void HandleCanvasMoved(object sender, MouseEventArgs e)
        {
            if (!_isPressed)
                return;

            if (this.GetCurrentMode() == Mode.Select)
            {
                Shape selectedShape;
                if (_isShape)
                {
                    if (currentShape != null)
                    {
                        selectedShape = currentShape;
                    }
                    else
                    {
                        selectedShape = Shapes.FirstOrDefault(
                        s => IsInRange(s.GetPoint1(), s.GetPoint2(), new Position(e.X, e.Y))
                        );
                        currentShape = selectedShape;
                    }
                }
                else
                {
                    return;
                }                
                
                if (selectedShape == null)
                    return;

                var mouseDelta = new Position(e.X - _previousMousePosition.X, e.Y - _previousMousePosition.Y);
                _previousMousePosition.X = e.X;
                _previousMousePosition.Y = e.Y;

                selectedShape.SetPoint1(new Position(selectedShape.GetPoint1().X + mouseDelta.X, selectedShape.GetPoint1().Y + mouseDelta.Y));
                selectedShape.SetPoint2(new Position(selectedShape.GetPoint2().X + mouseDelta.X, selectedShape.GetPoint2().Y + mouseDelta.Y));

                NotifyModelChanged();
                return;
            }

            _previewShape.SetPoint2(new Position(e.X, e.Y));
            NotifyModelChanged();
        }

        /// <summary>
        /// released
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void HandleCanvasReleased(object sender, MouseEventArgs e)
        {
            if (!_isPressed)
                return;

            _isPressed = false;
            currentShape = null;
            if (this.GetCurrentMode() == Mode.Select)
            {
                
                return;
            }

            _previewShape.SetPoint2(new Position(e.X, e.Y));
            Shapes.Add(ShapeFactory.CreateShape(SelectedShape, _previewShape.GetPoint1(), _previewShape.GetPoint2()));

            _previewShape = null;
            this.SetCurrentMode(Mode.Select);
            NotifyModelChanged();
        }

        private Shape _previewShape;
        private bool _isPressed;
        private Mode currentMode;
        private bool _isShape;
        Shape currentShape;

        public BindingList<Shape> Shapes
        {
            get;
            private set;
        }

        public ShapeType SelectedShape
        {
            get;
            set;
        }

        public Mode CurrentMode{ get => currentMode; set => currentMode = value; }

        //public Mode CurrentMode;
        /// <summary>
        /// released
        /// </summary>
        public void SetCurrentMode(Mode value)
        {
            currentMode = value;
        }

        //public Mode CurrentMode;
        /// <summary>
        /// released
        /// </summary>
        public Mode GetCurrentMode()
        {
            return currentMode;
        }

        private Position _previousMousePosition;
    }
}
