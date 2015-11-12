using System;
using System.Diagnostics;
using MyFirstBatchApplication.Batch;
using Summer.Batch.Core;

namespace MyFirstBatchApplication
{
    /// <summary>
    /// Main entry point
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Launches the job
        /// </summary>
        /// <param name="args">The arguments for the job execution</param>
        /// <returns></returns>
        public static int  Main(string[] args)
        {
#if DEBUG
            var stopwatch = new Stopwatch();
            stopwatch.Start();
#endif
            JobExecution jobExecution = JobStarter.Start(@"Batch\MyFirstBatch.xml", new MyFirstBatchUnityLoader());

#if DEBUG
            stopwatch.Stop();
            Console.WriteLine(Environment.NewLine + "Done in {0}ms.", stopwatch.ElapsedMilliseconds);
            Console.WriteLine("Press a key to end.");
            Console.ReadKey();
#endif

            return (int)(jobExecution.Status == BatchStatus.Completed ? JobStarter.Result.Success : JobStarter.Result.Failed);
        }
    }
}
