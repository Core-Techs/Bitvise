using System.Configuration;
using System.ServiceProcess;

namespace CoreTechs.BitVise.Host
{
    public partial class BitviseWinService : ServiceBase
    {
        private readonly AppHost _appHost;

        public BitviseWinService()
        {
            InitializeComponent();
            _appHost = new AppHost();
            _appHost.Init();
        }

        public void Start(string[] args)
        {
            OnStart(args);
        }

        protected override void OnStart(string[] args)
        {
            var url = ConfigurationManager.AppSettings["url"];

            _appHost.Start(url);
        }

        protected override void OnStop()
        {
            _appHost.Stop();
        }
    }
}
