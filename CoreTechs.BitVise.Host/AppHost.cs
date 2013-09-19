using System;
using Funq;
using ServiceStack.ServiceInterface;
using ServiceStack.WebHost.Endpoints;

namespace CoreTechs.BitVise.Host
{
    public class AppHost : AppHostHttpListenerBase
    {
        public AppHost()
            : base("who cares", typeof(VirtAccountService).Assembly)
        {

        }


        public override void Configure(Container container)
        {
            Routes.Add<VirtAccountRequest>("/virtAccount");
        }
    }

    public class VirtAccountService : Service
    {
        public object Any(VirtAccountRequest request)
        {
            throw new NotImplementedException();
        }
    }

    public class VirtAccountRequest
    {
        public string Username { get; set; }
        public string NewPassword { get; set; }
        public string Group { get; set; }
    }
}