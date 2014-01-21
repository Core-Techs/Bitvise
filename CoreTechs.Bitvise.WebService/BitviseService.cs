using System.ServiceProcess;

namespace CoreTechs.Bitvise.WebService
{
    partial class BitviseService : ServiceBase
    {
        public BitviseService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            // start web server
        }

        protected override void OnStop()
        {
            // stop web server
        }
    }
}
