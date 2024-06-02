using NUnit.Framework;
using NUnit.Framework.Legacy;
using TPW_Project.Model;
using TPW_Project.ViewModel;

namespace TPW_Project.Tests
{
    [TestFixture]
    public class BallViewModelTests
    {
        [Test]
        public void MoveWithinBoundsShouldUpdateCoordinatesTest()
        {
            var ball = new Ball(100, 100, 10, 10, 20.0d, 10.0d);
            var viewModel = new BallController(ball);

            viewModel.Move();

            ClassicAssert.AreEqual(110, viewModel.CoordinateX);
            ClassicAssert.AreEqual(110, viewModel.CoordinateY);
        }

        [Test]
        public void MoveHitsLeftBoundaryShouldReverseSpeedXTest()
        {
            var ball = new Ball(0, 100, -10, 0, 20.0d, 10.0d); 
            var viewModel = new BallController(ball);

            viewModel.Move();

            ClassicAssert.AreEqual(10, viewModel.SpeedX); 
        }

        [Test]
        public void MoveHitsTopBoundaryShouldReverseSpeedYTest()
        { 
            var ball = new Ball(100, 0, 0, -10, 20.0d, 10.0d); 
            var viewModel = new BallController(ball);
            
            viewModel.Move();

            ClassicAssert.AreEqual(10, viewModel.SpeedY); 
        }
    }
}
