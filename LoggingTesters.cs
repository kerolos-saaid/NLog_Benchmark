using NLog;
using NLog.Config;

namespace LoggingApp
{
    public class LoggingTesters
    {
        static LoggingTesters()
        {
            var config = new XmlLoggingConfiguration("NLog.config");
            LogManager.Configuration = config;
        }

        public static async Task AsyncLoggingTest(int numberOfLogs)
        {
            Logger logger = LogManager.GetLogger("AsyncLogger");
            Task[] tasks = new Task[numberOfLogs];
            for (int i = 0; i < numberOfLogs; i++)
            {
                int index = i; // Capture the loop variable
                tasks[i] = Task.Run(() => logger.Info($"Log line {index}"));
            }

            await Task.WhenAll(tasks);

            // Create a task that will be completed once the logging system is flushed
            var flushCompletionTask = Task.Run(() => LogManager.Flush());

            // Wait for the flush operation to complete
            await flushCompletionTask;

            // Shutdown the logging system
            LogManager.Shutdown();
        }

        public static void SyncLoggingTest(int numberOfLogs)
        {
            Logger logger = LogManager.GetLogger("SyncLogger");

            for (int i = 0; i < numberOfLogs; i++)
            {
                logger.Info("Log line " + i);
            }
        }
    }
}
