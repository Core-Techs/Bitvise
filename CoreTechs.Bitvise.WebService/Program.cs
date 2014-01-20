using CoreTechs.Bitvise.WebService;
using CoreTechs.Bitvise.WebService.Infrastructure;
using CoreTechs.Logging;
using System;
using System.ServiceProcess;

namespace AFS.BatchService
{
    static class Program
    {
        private static readonly Logger Log = SetupLogging().CreateLogger();

        private static LogManager SetupLogging()
        {
            return LogManager.Global = LogManager.Configure("logging");
        }

        private static void Main(string[] args)
        {
            var singleGlobalInstance = Attempt.Get(() => new SingleGlobalInstance()).Value;
            if (singleGlobalInstance == null)
            {
                if (Environment.UserInteractive)
                {
                    Console.WriteLine("Another instance of the application is running. Press any key to exit.");
                    Console.ReadKey();
                }
                return;
            }

            using (singleGlobalInstance)
            {

                var svc = new BitviseService();
                var svcRunner = new ServiceRunner(args, svc);
                svcRunner.Start();

                // wait for any straggling async log entries
                LogManager.Global.Dispose();
            }
        }
    }
}