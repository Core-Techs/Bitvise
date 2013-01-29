using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;
using BssCfg554Lib;
using JetBrains.Annotations;

namespace CoreTechs.Bitvise
{
    public static class Extensions
    {
        [StringFormatMethod("query")]
        public static string Query([NotNull] this BssCfg554 server, [NotNull] string query, params object[] args)
        {
            if (server == null) throw new ArgumentNullException("server");
            if (query == null) throw new ArgumentNullException("query");

            return server.QueryValue(string.Format(query, args), 0);
        }

        [StringFormatMethod("command")]
        public static void Command([NotNull] this BssCfg554 server, [NotNull] string command, params object[] args)
        {
            if (server == null) throw new ArgumentNullException("server");
            if (command == null) throw new ArgumentNullException("command");

            server.ProcessInstruction(string.Format(command, args));
        }

        public static void LockServerSettings([NotNull] this BssCfg554 server, TimeSpan? timeout)
        {
            if (server == null) throw new ArgumentNullException("server");
            var sw = Stopwatch.StartNew();
            Exception innerException;

            var retryWait = TimeSpan.FromMilliseconds(300);
            var tries = 0;

            while (true)
            {
                try
                {
                    server.LockServerSettings();
                    return;
                }
                catch (COMException ex)
                {
                    tries++;
                    innerException = ex;

                    if (timeout == null || timeout < sw.Elapsed)
                        break;

                    var newRetryWait = timeout.Value - sw.Elapsed;
                    if (newRetryWait < retryWait)
                        retryWait = newRetryWait;

                    Thread.Sleep(retryWait);
                }
            }

            throw new BitviseServerSettingsLockingException(
                string.Format(
                    "Could not obtain server settings lock after {0} tries over the course of {1:n2} seconds.", tries,
                    sw.Elapsed.TotalSeconds), innerException);
        }
    }
}