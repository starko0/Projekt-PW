using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TPW_Project.ViewModelLogic;

namespace TPW_Project.Controllers
{
    public class LoggingController
    {
        private const string logPath = "log.json";

        private DateTime logTime;
        private BallListController ballListToLog;

        public LoggingController(BallListController ballListInfo)
        {
            ballListToLog = ballListInfo;
        }

        public void LogBallList(BallListController ballList)
        {
            logTime = DateTime.Now;
            ballListToLog = ballList;
            
            var logEntry = new {
                logTime = logTime,
                ballList = ballListToLog
            };

            string jsonString = JsonSerializer.Serialize(logEntry);
            TryWriteLog(jsonString);
        }
        
        private async void TryWriteLog (string content)
        {
            int attempts = 0;
            bool success = false;
            const int maxRetryAttempts = 10;

            var logEntry = new
            {
                logTime = logTime,
                ballList = ballListToLog
            };

            while (attempts<maxRetryAttempts && !success)
            {
                try
                {
                    File.AppendAllText(logPath, content);
                    success = true;
                }
                catch (IOException ex)
                {
                    attempts++;
                    Console.WriteLine($"Attempt {attempts} failed: {ex.Message}");
                    if(attempts< maxRetryAttempts)
                    {
                        await Task.Delay(5);
                    }
                }
            }
            if(!success)
            {
                Console.WriteLine("Failed to write log entry to file");
            }
        }
    }
}
