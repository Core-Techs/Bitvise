using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreTechs.Logging;
using AppFunc = System.Func<System.Collections.Generic.IDictionary<string, object>, System.Threading.Tasks.Task>;

namespace CoreTechs.Bitvise.WebService.Infrastructure
{
    public class RequestLogger
    {
        private readonly AppFunc _next;
        private readonly Logger _log;
        private readonly string[] _keys;

        public RequestLogger(AppFunc next, LogManager logManager)
        {
            if (next == null) throw new ArgumentNullException("next");
            if (logManager == null) throw new ArgumentNullException("logManager");

            _next = next;
            _log = logManager.CreateLogger();

            _keys = new[]
            {
                "owin.RequestPath",
                "owin.ResponseStatusCode",
                "owin.RequestQueryString",
                "owin.RequestMethod",
                "owin.RequestScheme",
                "owin.RequestPathBase",
                "owin.RequestProtocol",
                "server.User",
                "server.RemoteIpAddress",
                "server.RemotePort",
            };
        }

        public Task Invoke(IDictionary<string, object> environment)
        {
            var lb = new LogEntryBuilder(_log);

            foreach (var key in _keys.Where(environment.ContainsKey))
                lb.Data(key, environment[key]);

            lb.Trace("Request");
            return _next(environment);
        }
    }
}