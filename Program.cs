using System.Diagnostics;

namespace LoggingApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            int numberOfLogs = 1000000;

            // Benchmark synchronous logging
            Stopwatch stopwatch = Stopwatch.StartNew();
            LoggingTesters.SyncLoggingTest(numberOfLogs);

            stopwatch.Stop();
            Console.WriteLine($"Synchronous logging took {stopwatch.ElapsedMilliseconds} ms");

            // Benchmark asynchronous logging
            stopwatch.Restart();
            await LoggingTesters.AsyncLoggingTest(numberOfLogs);
            stopwatch.Stop();
            Console.WriteLine($"Asynchronous logging took {stopwatch.ElapsedMilliseconds} ms");
        }
    }
}
