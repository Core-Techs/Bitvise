using System;
using System.ServiceProcess;
using CoreTechs.Bitvise.WebService.Infrastructure;
using CoreTechs.Logging;
using Microsoft.Owin.Hosting;

namespace CoreTechs.Bitvise.WebService
{
    partial class BitviseService : ServiceBase
    {
        private static readonly Logger Log = LogManager.Global.CreateLogger();
        private IDisposable _httpServer;

        public BitviseService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            _httpServer = WebApp.Start<WebApiStartup>(AppSettings.ServiceUrl);
            Log.Data("URL",AppSettings.ServiceUrl).Info("Started HTTP server.");
        }

        protected override void OnStop()
        {
            _httpServer.Dispose();
        }
    }
}