using Microsoft.VisualStudio.TestTools.UnitTesting;
using PowerPoint;
using System.Drawing;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PowerPoint.Tests
{
    [TestClass()]
    public class ViewModelTests
    {
        const int ZERO = 0;
        const int ONE = 1;
        //test
        [TestMethod()]
        public void ViewModelTest()
        {
            ViewModel viewModel = new ViewModel();
            Assert.AreEqual(viewModel.Shapes.Count,
                ZERO);
        }

        //test
        [TestMethod()]
        public void AddTest()
        {
            ViewModel viewModel = new ViewModel();
            Position point1 = new Position(ZERO, ZERO);
            Position point2 = new Position(ZERO, ZERO);
            Shape shape = ShapeFactory.CreateShape(ShapeType.Ellipse, point1, point2);
            viewModel.Add(shape);
            Assert.AreEqual(viewModel.Shapes[0],
                shape);
        }
        //test
        [TestMethod()]
        public void RemoveAtTest()
        {
            ViewModel viewModel = new ViewModel();
            Position point1 = new Position(ZERO, ZERO);
            Position point2 = new Position(ZERO, ZERO);
            Shape shape = ShapeFactory.CreateShape(ShapeType.Ellipse, point1, point2);
            viewModel.Shapes.Add(shape);
            point1 = new Position(ONE, ONE);
            point2 = new Position(ZERO, ZERO);
            shape = ShapeFactory.CreateShape(ShapeType.Line, point1, point2);
            viewModel.Add(shape);
            viewModel.RemoveAt(ZERO);
            Assert.AreEqual(viewModel.Shapes[0].Name,
                "Line");
        }
        //test
        [TestMethod()]
        public void SetCurrentModeTest()
        {
            ViewModel viewModel = new ViewModel();
            viewModel.SetCurrentMode(Mode.Draw);
            Assert.AreEqual(viewModel.CurrentMode,
               Mode.Draw);
        }
        //test
        [TestMethod()]
        public void GetCurrentModeTest()
        {
            ViewModel viewModel = new ViewModel();
            viewModel.SetCurrentMode(Mode.Draw);
            viewModel.SelectedShape = ShapeType.Ellipse;
            Assert.AreEqual(viewModel.GetCurrentMode(),
               Mode.Draw);
        }
        //test
        [TestMethod()]
        public void DrawTest()
        {
            ViewModel viewModel = new ViewModel();
            Position point1 = new Position(ZERO, ZERO);
            Position point2 = new Position(ZERO, ZERO);
            Shape shape = ShapeFactory.CreateShape(ShapeType.Ellipse, point1, point2);
            viewModel.Shapes.Add(shape);
            point1 = new Position(ONE, ONE);
            point2 = new Position(ZERO, ZERO);
            shape = ShapeFactory.CreateShape(ShapeType.Line, point1, point2);
            viewModel.Add(shape);

            Panel panel = new Panel();
            Graphics graphic = panel.CreateGraphics();
            viewModel.Draw(graphic);


        }
        //test
        [TestMethod()]
        public void DeleteSelectedTest()
        {
            ViewModel viewModel = new ViewModel();
            Position point1 = new Position(ZERO, ZERO);
            Position point2 = new Position(ZERO, ZERO);
            Shape shape = ShapeFactory.CreateShape(ShapeType.Ellipse, point1, point2);
            viewModel.Shapes.Add(shape);
            viewModel.Shapes[0].SetSelected(true);
            viewModel.DeleteSelected();
            Assert.AreEqual(viewModel.Shapes.Count,
                ZERO);
        }
        //test
        [TestMethod()]
        public void HandleCanvasPressedTest1()
        {
            MouseEventArgs mouse;
            mouse = new MouseEventArgs(MouseButtons.Left, ZERO, ZERO, ZERO, ZERO);
            ViewModel viewModel = new ViewModel();
            Position point1 = new Position(ZERO, ZERO);
            Position point2 = new Position(ZERO, ZERO);
            Shape shape = ShapeFactory.CreateShape(ShapeType.Ellipse, point1, point2);
            viewModel.Shapes.Add(shape);
            viewModel.HandleCanvasPressed(null, mouse);
            mouse = new MouseEventArgs(MouseButtons.Left, ZERO, ONE, ONE, ZERO);
            viewModel.HandleCanvasPressed(null, mouse);

        }
        //test
        [TestMethod()]
        public void HandleCanvasMovedTest1()
        {
            MouseEventArgs mouse;
            ViewModel viewModel = new ViewModel();
            Position point1 = new Position(0, 0);
            Position point2 = new Position(-10, -10);
            Shape shape = ShapeFactory.CreateShape(ShapeType.Ellipse, point1, point2);
            viewModel.Shapes.Add(shape);
            viewModel.Shapes[0].SetSelected(true);
            viewModel.CurrentMode = Mode.Draw;
            for (int i = 1; i < 100; i++)
            {
                mouse = new MouseEventArgs(MouseButtons.Left, ZERO, i, i, ZERO);
                viewModel.HandleCanvasPressed(null, mouse);
                viewModel.HandleCanvasMoved(null, mouse);
            }
            mouse = new MouseEventArgs(MouseButtons.Left, ZERO, 100, 100, ZERO);
            viewModel.HandleCanvasReleased(null, mouse);
            Assert.AreEqual(viewModel.Shapes.Count,
                2);

        }
        //test
        [TestMethod()]
        public void HandleCanvasReleasedTest1()
        {
            MouseEventArgs mouse;
            ViewModel viewModel = new ViewModel();
            Position point1 = new Position(-50, -50);
            Position point2 = new Position(50, 50);
            Shape shape = ShapeFactory.CreateShape(ShapeType.Ellipse, point1, point2);
            viewModel.Shapes.Add(shape);
            viewModel.Shapes[0].SetSelected(true);
            viewModel.CurrentMode = Mode.Select;
            for (int i = 0; i < 100; i++)
            {
                mouse = new MouseEventArgs(MouseButtons.Left, ZERO, i, i, ZERO);
                viewModel.HandleCanvasPressed(null, mouse);
                viewModel.HandleCanvasMoved(null, mouse);
            }
            mouse = new MouseEventArgs(MouseButtons.Left, ZERO, 100, 100, ZERO);
            viewModel.HandleCanvasReleased(null, mouse);


        }

        //test
        [TestMethod()]
        public void FormLoadTest()
        {
            Form1 form = new Form1
            {
                BackColor = Color.Red
            };
            Assert.AreEqual(form.BackColor,
                Color.Red);
        }
    }
}