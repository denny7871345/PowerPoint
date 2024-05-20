using Microsoft.VisualStudio.TestTools.UnitTesting;
using PowerPoint;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PowerPoint.Tests
{

    [TestClass()]
    public class ShapeTests
    {
        const int ZERO = 0;
        const int ONE = 1;
        //GetPoint1Test
        [TestMethod()]
        public void GetPoint1Test()
        {
            Position point1 = new Position(ZERO, ZERO);
            Position point2 = new Position(ZERO, ZERO);
            Shape shape = ShapeFactory.CreateShape(ShapeType.Ellipse, point1, point2);
            shape.SetSelected(true);
            Assert.AreEqual(shape.GetPoint1(), point1);
        }
        //Test
        [TestMethod()]
        public void GetPoint2Test()
        {
            Position point1 = new Position(ZERO, ZERO);
            Position point2 = new Position(ZERO, ZERO);
            Shape shape = ShapeFactory.CreateShape(ShapeType.Ellipse, point1, point2);
            Assert.AreEqual(shape.GetPoint2(), point2);
        }
        //Test
        [TestMethod()]
        public void SetPoint1Test()
        {
            Position point1 = new Position(ZERO, ZERO);
            Position point2 = new Position(ZERO, ZERO);
            Shape shape = ShapeFactory.CreateShape(ShapeType.Ellipse, point1, point2);
            point1 = new Position(ONE, ONE);
            shape.SetPoint1(point1);
            Assert.AreEqual(shape.GetPoint1(), point1);
        }
        //Test
        [TestMethod()]
        public void SetPoint2Test()
        {
            Position point1 = new Position(ZERO, ZERO);
            Position point2 = new Position(ZERO, ZERO);
            Shape shape = ShapeFactory.CreateShape(ShapeType.Ellipse, point1, point2);
            point2 = new Position(ONE, ONE);
            shape.SetPoint2(point2);
            Assert.AreEqual(shape.GetPoint2(), point2);
        }
        //Test
        [TestMethod()]
        public void IsSelectedTest()
        {
            Position point1 = new Position(ZERO, ZERO);
            Position point2 = new Position(ZERO, ZERO);
            Shape shape = ShapeFactory.CreateShape(ShapeType.Ellipse, point1, point2);
            shape.SetSelected(true);
            Assert.AreEqual(shape.IsSelected(),
                true);
        }
        //Test
        [TestMethod()]
        public void SetSelectedTest()
        {
            Position point1 = new Position(ZERO, ZERO);
            Position point2 = new Position(ZERO, ZERO);
            Shape shape = ShapeFactory.CreateShape(ShapeType.Ellipse, point1, point2);
            shape.SetSelected(false);
            Assert.AreEqual(shape.IsSelected(),
                false);
        }
        //test
        [TestMethod()]
        public void DrawTest()
        {
            Panel panel = new Panel();
            Position point1 = new Position(ZERO, ZERO);
            Position point2 = new Position(ZERO, ZERO);
            Shape shape = ShapeFactory.CreateShape(ShapeType.Ellipse, point1, point2);
            Graphics graphic = panel.CreateGraphics();
            shape.SetSelected(true);
            shape.Draw(graphic);
            shape = ShapeFactory.CreateShape(ShapeType.Line, point1, point2);
            shape.SetSelected(true);
            shape.Draw(graphic);
            shape = ShapeFactory.CreateShape(ShapeType.Rectangle, point1, point2);
            shape.SetSelected(true);
            shape.Draw(graphic);
            Assert.AreEqual(shape.IsSelected(),
                true);
        }
    }
}