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
            int initialX = 10;
            int initialY = 20;
            int initialSpeedX = 5;
            int initialSpeedY = -5;

            Ball ball = new Ball(initialX, initialY, initialSpeedX, initialSpeedY);

            ClassicAssert.AreEqual(initialX, ball.CoordinateX);
            ClassicAssert.AreEqual(initialY, ball.CoordinateY);
            ClassicAssert.AreEqual(initialSpeedX, ball.SpeedX);
            ClassicAssert.AreEqual(initialSpeedY, ball.SpeedY);
        }

        [Test]
        public void PropertiesChangeTest()
        {
            Ball ball = new Ball(0, 0, 0, 0);
            int newX = 100;
            int newY = 200;
            int newSpeedX = 50;
            int newSpeedY = -50;

            ball.CoordinateX = newX;
            ball.CoordinateY = newY;
            ball.SpeedX = newSpeedX;
            ball.SpeedY = newSpeedY;

            ClassicAssert.AreEqual(newX, ball.CoordinateX);
            ClassicAssert.AreEqual(newY, ball.CoordinateY);
            ClassicAssert.AreEqual(newSpeedX, ball.SpeedX);
            ClassicAssert.AreEqual(newSpeedY, ball.SpeedY);
        }
    }
}
