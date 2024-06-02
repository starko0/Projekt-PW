using NUnit.Framework;
using NUnit.Framework.Legacy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPW_Project.ViewModel;

namespace TPW_Project.UnitTests
{
    [TestFixture]
    public class SimualtionControllerTests
    {
        private SimualtionController simualtionController;

        [SetUp]
        public void Setup()
        {
            simualtionController = new SimualtionController();
        }

        [Test]
        public void Constructor_InitializesProperties()
        {
            // ClassicAssert
            ClassicAssert.IsNotNull(simualtionController.BallList);
            ClassicAssert.IsNotNull(simualtionController.logController);
            ClassicAssert.IsNotNull(simualtionController.StartButton);
            ClassicAssert.IsNotNull(simualtionController.SubmitButton);
            ClassicAssert.AreEqual("Start", simualtionController.StartButtonText);
            ClassicAssert.AreEqual(string.Empty, simualtionController.ProgramStatusText);
            ClassicAssert.AreEqual(string.Empty, simualtionController.SubmitInputText);
            ClassicAssert.AreEqual(System.Windows.Visibility.Visible, simualtionController.SubmitBasicTextVisibility);
        }

        [Test]
        public void SubmitInputText_ValidInput_UpdatesProperties()
        {
            // Act
            simualtionController.SubmitInputText = "5";

            // ClassicAssert
            ClassicAssert.AreEqual("5", simualtionController.SubmitInputText);
            ClassicAssert.AreEqual(System.Windows.Visibility.Collapsed, simualtionController.SubmitBasicTextVisibility);
        }

        [Test]
        public void SubmitInputText_InvalidInput_ResetsProperties()
        {
            // Act
            simualtionController.SubmitInputText = "invalid";

            // ClassicAssert
            ClassicAssert.AreEqual(string.Empty, simualtionController.SubmitInputText);
            ClassicAssert.AreEqual(System.Windows.Visibility.Visible, simualtionController.SubmitBasicTextVisibility);
        }

        [Test]
        public async Task MoveBallsAsync_StartsAndStopsSimulation()
        {
            // Arrange
            simualtionController.BallList.GenerateBalls(2);
            simualtionController.IsSimulationRunning = true;

            // Act
            var moveBallsTask = simualtionController.MoveBallsAsync();
            await Task.Delay(100); // Pozwól na kilka iteracji
            simualtionController.IsSimulationRunning = false;
            await moveBallsTask;

            // ClassicAssert
            ClassicAssert.IsFalse(simualtionController.IsSimulationRunning);
        }
    }
}
