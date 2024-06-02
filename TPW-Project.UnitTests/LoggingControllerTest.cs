using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using NUnit.Framework.Legacy;
using TPW_Project.Controllers;
using TPW_Project.ViewModelLogic;


namespace TPW_Project.UnitTests
{
    public class LoggingControllerTest
    {
        private const string logPath = "log.json";

        [SetUp]
        public void Setup()
        {
            if(File.Exists(logPath))
            {
                File.Delete(logPath);
            }   
        }

        [Test]
        public void LogBallListTest()
        {
            var ballList = new BallListController();
            var loggingController = new LoggingController(ballList);

            loggingController.LogBallList(ballList);

            ClassicAssert.IsTrue(File.Exists(logPath));
        }
        [Test]
        public void LogBallListTest_ContainsCorrectData()
        {
            var ballList = new BallListController();
            var loggingController = new LoggingController(ballList);

            loggingController.LogBallList(ballList);

            string logContent = File.ReadAllText(logPath);

            ClassicAssert.IsTrue(logContent.Contains("ballList"));
            ClassicAssert.IsTrue(logContent.Contains("logTime"));
        }

    }
}
