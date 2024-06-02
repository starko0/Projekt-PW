using NUnit.Framework;
using NUnit.Framework.Legacy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPW_Project.Controllers;
using TPW_Project.ViewModel;
using TPW_Project.ViewModelLogic;

namespace TPW_Project.UnitTests
{
    [TestFixture]
    public class IntegrationTests
    {
        private MainController mainController;
        private SimualtionController simualtionController;
        private BallListController ballListController;
        private LoggingController loggingController;

        [SetUp]
        public void Setup()
        {
            mainController = new MainController();
            simualtionController = (SimualtionController)mainController.CurrentViewModel;
            ballListController = simualtionController.BallList;
            loggingController = simualtionController.logController;
        }

        [Test]
        public void MainController_InitializesSimualtionController()
        {
            // ClassicAssert
            ClassicAssert.IsNotNull(mainController.CurrentViewModel);
            ClassicAssert.IsInstanceOf<SimualtionController>(mainController.CurrentViewModel);
        }

        [Test]
        public void SimualtionController_InitializesBallListController()
        {
            // ClassicAssert
            ClassicAssert.IsNotNull(simualtionController.BallList);
            ClassicAssert.IsInstanceOf<BallListController>(simualtionController.BallList);
        }

        [Test]
        public void SimualtionController_InitializesLoggingController()
        {
            // ClassicAssert
            ClassicAssert.IsNotNull(simualtionController.logController);
            ClassicAssert.IsInstanceOf<LoggingController>(simualtionController.logController);
        }

        [Test]
        public async Task SimualtionController_StartsAndStopsSimulation()
        {
            // Arrange
            ballListController.GenerateBalls(2);
            simualtionController.IsSimulationRunning = true;

            // Act
            var moveBallsTask = simualtionController.MoveBallsAsync();
            await Task.Delay(100); // Pozwól na kilka iteracji
            simualtionController.IsSimulationRunning = false;
            await moveBallsTask;

            // ClassicAssert
            ClassicAssert.IsFalse(simualtionController.IsSimulationRunning);
        }

        [Test]
        public void BallListController_GeneratesBallsAndChecksCollisions()
        {
            // Arrange
            int amount = 10;

            // Act
            ballListController.GenerateBalls(amount);
            var collisions = ballListController.CheckCollisionBetweenBalls();

            // ClassicAssert
            ClassicAssert.AreEqual(amount, ballListController.Balls.Count);

            // Jeśli są kolizje, sprawdź reakcje na kolizje
            if (collisions.Count > 0)
            {
                foreach (var collision in collisions)
                {
                    ballListController.reactionOnCollision(collision);
                }
            }

            // Sprawdź, czy po reakcji na kolizje nie ma już kolizji
            var postCollisionCheck = ballListController.CheckCollisionBetweenBalls();
            ClassicAssert.IsEmpty(postCollisionCheck);
        }

        [Test]
        public void LoggingController_LogsBallList()
        {
            // Arrange
            ballListController.GenerateBalls(2);

            // Act
            loggingController.LogBallList(ballListController);

            ClassicAssert.Pass();
        }
    }
}
