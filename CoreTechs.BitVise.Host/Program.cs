using System;
using System.ServiceProcess;

namespace CoreTechs.BitVise.Host
{
    static class Program
    {
        static void Main(string[] args)
        {
            var service = new BitviseWinService();

            if (Environment.UserInteractive)
            {
                service.Start(args);
                Console.WriteLine("Press Enter to stop the service...");
                Console.ReadLine();
                service.Stop();
                Console.WriteLine("The service is stopped. Press Enter to terminate...");
                Console.ReadLine();
            }
            else
            {
                ServiceBase.Run(service);
            }
        }
    }
}