using NUnit.Framework;
using NUnit.Framework.Legacy;
using TPW_Project.ViewModel;

namespace TPW_Project.Tests
{
    [TestFixture]
    public class SimulationViewModelTests
    {
        [Test]
        public void GenerateBallsTest() { 
        
            var simulationVM = new SimulationViewModel();
            int ballsToGenerate = 5;

            simulationVM.GenerateBalls(ballsToGenerate);

            ClassicAssert.AreEqual(ballsToGenerate, simulationVM.Balls.Count);
        }

        [Test]
        public void CheckCollisionReturnsFalseTest()
        {         
            var simulationVM = new SimulationViewModel();
            int testX = 100;
            int testY = 100;

            bool result = simulationVM.CheckCollision(testX, testY);

            ClassicAssert.IsFalse(result);
        }

        [Test]
        public void CheckCollisionWhenCollisionOccursTest()
        {
            var simulationVM = new SimulationViewModel();
            simulationVM.GenerateBalls(1); // Generowanie jednej kulki
            int testX = simulationVM.Balls[0].CoordinateX; // Użycie tej samej pozycji X
            int testY = simulationVM.Balls[0].CoordinateY; // i Y dla testu

            bool result = simulationVM.CheckCollision(testX, testY);

            ClassicAssert.IsTrue(result);
        }
    }
}
