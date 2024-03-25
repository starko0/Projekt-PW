using NUnit.Framework;
using TPW_Project.Model;

namespace TPW_Project.UnitTests
{
    class BallTests
    {
        private Ball ball;

        [SetUp]
        public void SetUp()
        {
            ball = new Ball(50, 50, 3, 3);
        }
        [Test]
        public void GetterAndSetterTest()
        {
            // Arrange
            int expected = 5;
            int actual = 2 + 3;

            // Act

            // Assert
            Assert.A
        }
    }
}
