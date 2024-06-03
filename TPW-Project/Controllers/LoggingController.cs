using System.IO;
using System.Text.Json;
using TPW_Project.ViewModelLogic;

public class LoggingController
{
    private const string logPath = "log.json";
    private Timer logTimer;
    private BallListController ballListToLog;
    private readonly object lockObject = new object();

    public LoggingController(BallListController ballListInfo, int logInterval)
    {
        ballListToLog = ballListInfo;
        logTimer = new Timer(LogBallList, null, logInterval, logInterval);
    }

    private void LogBallList(object state)
    {
        lock (lockObject)
        {
            var logTime = DateTime.Now;
            var logEntry = new
            {
                logTime = logTime,
                ballList = ballListToLog
            };

            string jsonString = JsonSerializer.Serialize(logEntry);
            TryWriteLog(jsonString);
        }
    }

    private async void TryWriteLog(string content)
    {
        int attempts = 0;
        bool success = false;
        const int maxRetryAttempts = 10;

        while (attempts < maxRetryAttempts && !success)
        {
            try
            {
                await File.AppendAllTextAsync(logPath, content + Environment.NewLine);
                success = true;
            }
            catch (IOException ex)
            {
                attempts++;
                Console.WriteLine($"Attempt {attempts} failed: {ex.Message}");
                if (attempts < maxRetryAttempts)
                {
                    await Task.Delay(5);
                }
            }
        }

        if (!success)
        {
            Console.WriteLine("Failed to write log entry to file");
        }
    }

    public void StopLogging()
    {
        logTimer?.Change(Timeout.Infinite, Timeout.Infinite);
        logTimer?.Dispose();
    }
}
