using NUnit.Framework;
using NUnit.Framework.Legacy;
using TPW_Project.Model;

namespace TPW_Project.Tests
{
    [TestFixture]
    public class BallTests
    {
        [Test]
        public void ConstructorTest()
        {
            double initialX = 10;
            double initialY = 20;
            double initialSpeedX = 5;
            double initialSpeedY = -5;
            double initialWeight = 1;
            double initialRadius = 1;

            Ball ball = new Ball(initialX, initialY, initialSpeedX, initialSpeedY, initialWeight, initialRadius);

            ClassicAssert.AreEqual(initialX, ball.CoordinateX);
            ClassicAssert.AreEqual(initialY, ball.CoordinateY);
            ClassicAssert.AreEqual(initialSpeedX, ball.SpeedX);
            ClassicAssert.AreEqual(initialSpeedY, ball.SpeedY);
            ClassicAssert.AreEqual(initialWeight, ball.Weight);
            ClassicAssert.AreEqual(initialRadius, ball.Radius);
        }

        [Test]
        public void PropertiesChangeTest()
        {
            Ball ball = new Ball(0, 0, 0, 0,0,0);
            int newX = 100;
            int newY = 200;
            int newSpeedX = 50;
            int newSpeedY = -50;
            int newWeight = 1;
            int newRadius = 1;

            ball.CoordinateX = newX;
            ball.CoordinateY = newY;
            ball.SpeedX = newSpeedX;
            ball.SpeedY = newSpeedY;
            ball.Weight = newWeight;
            ball.Radius = newRadius;

            ClassicAssert.AreEqual(newX, ball.CoordinateX);
            ClassicAssert.AreEqual(newY, ball.CoordinateY);
            ClassicAssert.AreEqual(newSpeedX, ball.SpeedX);
            ClassicAssert.AreEqual(newSpeedY, ball.SpeedY);
            ClassicAssert.AreEqual(newWeight, ball.Weight);
            ClassicAssert.AreEqual(newRadius, ball.Radius);
        }
    }
}
