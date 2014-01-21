using System.Reflection;
using System.ServiceProcess;

namespace CoreTechs.Bitvise.WebService.Infrastructure
{
    static class Extensions
    {
        public static void Start(this ServiceBase service, params string[] args)
        {
            var onStart = typeof (ServiceBase).GetMethod("OnStart", BindingFlags.Instance | BindingFlags.NonPublic);
            onStart.Invoke(service, BindingFlags.Default, null, new object[] {args}, null);
        }
    }
}
