using System;
using System.Reflection;
using System.ServiceProcess;

namespace CoreTechs.Bitvise.WebService.Infrastructure
{
    /// <summary>
    /// Run your windows services as console apps during development!
    /// </summary>
    public class ServiceRunner
    {
        public ServiceBase[] Services { get; private set; }
        public string[] ServiceArgs { get; private set; }

        public ServiceRunner(string[] args = null, params ServiceBase[] services)
        {
            ServiceArgs = args;
            Services = services;
        }

        public void Start(string exitPrompt = "Press Enter to stop the service...")
        {
            if (Environment.UserInteractive)
            {
                var onStart = typeof(ServiceBase)
                    .GetMethod("OnStart", BindingFlags.Instance | BindingFlags.NonPublic);

                foreach (var svc in Services)
                    onStart.Invoke(svc, BindingFlags.Default, null, new object[] { ServiceArgs }, null);

                Console.WriteLine(exitPrompt);
                Console.ReadLine();
            }
            else
            {
                ServiceBase.Run(Services);
            }
        }

        public void Stop()
        {
            foreach (var svc in Services)
                svc.Stop();
        }
    }
}