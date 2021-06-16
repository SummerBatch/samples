using System;
using System.Configuration;
using System.Diagnostics;
using BA_DB_WRITER_batch.Batch;
using Summer.Batch.Core;

namespace BA_DB_WRITER_Launcher
{
    public class Program
    {
        public static int Main(string[] args)
        {
#if DEBUG
            var stopwatch = new Stopwatch();
            stopwatch.Start();
#endif
            JobExecution jobExecution;
            string hostname = ConfigurationManager.AppSettings["hostname"];
            // need to know where server located
            jobExecution = JobStarter.WorkerStart(@"Batch\Worker.xml", hostname, new UnityLoader());


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
