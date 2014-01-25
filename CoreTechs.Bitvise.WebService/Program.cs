using System;
using System.ServiceProcess;
using CoreTechs.Bitvise.Common;
using CoreTechs.Bitvise.WebService.Infrastructure;
using CoreTechs.Logging;

namespace CoreTechs.Bitvise.WebService
{
    static class Program
    {
        private static readonly Logger Log = SetupLogging();

        private static Logger SetupLogging()
        {
            var lm = LogManager.Global = LogManager.Configure("logging");
            var log = lm.CreateLogger();

            lm.UnhandledLoggingException +=
                (sender, args) => log.Exception(args.Exception).Error("Unhandled Logging Exception");

            return log;
        }

        private static void Main(string[] args)
        {
            var singleGlobalInstance = Attempt.Get(() => new SingleGlobalInstance()).Value;
            if (singleGlobalInstance == null)
            {
                if (!Environment.UserInteractive) return;
                Console.WriteLine("Another instance of the application is running. Press any key to exit.");
                Console.ReadKey();
                return;
            }

            using (singleGlobalInstance)
            {
                var svc = new BitviseService();

                if (Environment.UserInteractive)
                {
                    svc.Start(args);
                    Console.WriteLine("Press Enter to stop the service...");
                    Console.ReadLine();
                    svc.Stop();
                    Console.WriteLine("The service is stopped. Press Enter to terminate...");
                    Console.ReadLine();
                }
                else
                {
                    ServiceBase.Run(svc);
                }

                // wait for any straggling async log entries
                LogManager.Global.Dispose();
            }
        }
    }
}