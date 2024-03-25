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
            var ball = new Ball(100, 100, 10, 10);
            var viewModel = new BallViewModel(ball);

            viewModel.Move();

            ClassicAssert.AreEqual(110, viewModel.CoordinateX);
            ClassicAssert.AreEqual(110, viewModel.CoordinateY);
        }

        [Test]
        public void MoveHitsHorizontalBoundaryShouldReverseSpeedXTest()
        { 
            var ball = new Ball(365, 100, 10, 0); 
            var viewModel = new BallViewModel(ball);

            viewModel.Move();

            ClassicAssert.AreEqual(-10, viewModel.SpeedX);
        }

        [Test]
        public void MoveHitsVerticalBoundaryShouldReverseSpeedYTest()
        { 
            var ball = new Ball(100, 365, 0, 10); 
            var viewModel = new BallViewModel(ball);

            viewModel.Move();

            ClassicAssert.AreEqual(-10, viewModel.SpeedY);
        }

        [Test]
        public void MoveHitsLeftBoundaryShouldReverseSpeedXTest()
        {
            var ball = new Ball(0, 100, -10, 0); 
            var viewModel = new BallViewModel(ball);

            viewModel.Move();

            ClassicAssert.AreEqual(10, viewModel.SpeedX); 
        }

        [Test]
        public void MoveHitsTopBoundaryShouldReverseSpeedYTest()
        { 
            var ball = new Ball(100, 0, 0, -10); 
            var viewModel = new BallViewModel(ball);
            
            viewModel.Move();

            ClassicAssert.AreEqual(10, viewModel.SpeedY); 
        }
    }
}
