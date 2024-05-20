using Microsoft.VisualStudio.TestTools.UnitTesting;
using PowerPoint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerPoint.Tests
{
    [TestClass()]
    public class ShapeFactoryTests
    {
        const int ZERO = 0;
        //Factory test
        [TestMethod()]
        public void CreateShapeTest()
        {
            Position position = new Position(ZERO,ZERO);
            ShapeType shapeType = ShapeType.Ellipse;
            Shape shape = ShapeFactory.CreateShape(shapeType,position,position);
            Assert.AreEqual(shape.GetPoint1(),
                position);
            shape = ShapeFactory.CreateShape(ShapeType.Line, position, position);
            Assert.AreEqual(shape.GetPoint1(),
                position);
            shape = ShapeFactory.CreateShape(ShapeType.Rectangle, position, position);
            Assert.AreEqual(shape.GetPoint1(),
                position);
        }
    }
}