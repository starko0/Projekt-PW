using NUnit.Framework;
using NUnit.Framework.Legacy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPW_Project.Model;
using TPW_Project.ViewModelLogic;

namespace TPW_Project.UnitTests
{
    [TestFixture]
    public class BallListControllerTests
    {
        private BallListController ballListController;

        [SetUp]
        public void Setup()
        {
            ballListController = new BallListController();
        }

        [Test]
        public void GenerateBalls_AddsCorrectNumberOfBalls()
        {
            // Arrange
            int amount = 5;

            // Act
            ballListController.GenerateBalls(amount);

            // ClassicAssert
            ClassicAssert.AreEqual(amount, ballListController.Balls.Count);
        }

        [Test]
        public void CheckCollisionDuringGeneretingBalls_ReturnsFalseWhenNoCollision()
        {
            // Arrange
            double x = 50;
            double y = 50;
            double r = 10;

            // Act
            bool result = ballListController.CheckCollisionDuringGeneretingBalls(x, y, r);

            // ClassicAssert
            ClassicAssert.IsFalse(result);
        }

        [Test]
        public void CheckCollisionDuringGeneretingBalls_ReturnsTrueWhenCollision()
        {
            // Arrange
            ballListController.GenerateBalls(1);
            var existingBall = ballListController.Balls[0];
            double x = existingBall.CoordinateX;
            double y = existingBall.CoordinateY;
            double r = existingBall.Radius;

            // Act
            bool result = ballListController.CheckCollisionDuringGeneretingBalls(x, y, r);

            // ClassicAssert
            ClassicAssert.IsTrue(result);
        }

        [Test]
        public void CheckCollisionBetweenBalls_ReturnsEmptyListWhenNoCollision()
        {
            // Arrange
            ballListController.GenerateBalls(2);
            var ball1 = ballListController.Balls[0];
            var ball2 = ballListController.Balls[1];
            ball1.CoordinateX = 0;
            ball1.CoordinateY = 0;
            ball2.CoordinateX = 100;
            ball2.CoordinateY = 100;

            // Act
            var collisions = ballListController.CheckCollisionBetweenBalls();

            // ClassicAssert
            ClassicAssert.IsEmpty(collisions);
        }

        [Test]
        public void ReactionOnCollision_HandlesCollision()
        {
            // Arrange
            ballListController.GenerateBalls(2);
            var ball1 = ballListController.Balls[0];
            var ball2 = ballListController.Balls[1];
            ball1.CoordinateX = 0;
            ball1.CoordinateY = 0;
            ball2.CoordinateX = 0;
            ball2.CoordinateY = 0;
            var collidedBalls = new List<int> { 0, 1 };

            // Act
            ballListController.reactionOnCollision(collidedBalls);

            // ClassicAssert
            // Sprawdź, czy reakcja na kolizję została obsłużona poprawnie
            // (np. zmiana prędkości, zmiana pozycji, itp.)
        }

        [Test]
        public void ReactionOnCollisionBetter_HandlesCollision()
        {
            // Arrange
            ballListController.GenerateBalls(2);
            var ball1 = ballListController.Balls[0];
            var ball2 = ballListController.Balls[1];
            ball1.CoordinateX = 0;
            ball1.CoordinateY = 0;
            ball2.CoordinateX = 0;
            ball2.CoordinateY = 0;
            var collidedBalls = new List<int> { 0, 1 };

            // Act
            ballListController.reactionOnCollisionBetter(collidedBalls);

            // ClassicAssert
            // Sprawdź, czy reakcja na kolizję została obsłużona poprawnie
            // (np. zmiana prędkości, zmiana pozycji, itp.)
        }
    }
}
