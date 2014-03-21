using System;
using System.Runtime.InteropServices;
using CoreTechs.Bitvise.Common;
using JetBrains.Annotations;
using BssCfg604Lib;

namespace CoreTechs.Bitvise
{
    public static class Extensions
    {
        /// <summary>
        /// Like the QueryValue method, but with string format arguments.
        /// </summary>
        [StringFormatMethod("query")]
        public static string Query([NotNull] this BssCfg604 server, [NotNull] string query, params object[] args)
        {
            if (server == null) throw new ArgumentNullException("server");
            if (query == null) throw new ArgumentNullException("query");

            var formatedQuery = string.Format(query, args);
            var result = server.QueryValue(formatedQuery, 0);
            return result;
        }

        /// <summary>
        /// Like the ProcessInstruction method, but with string format arguments.
        /// </summary>
        [StringFormatMethod("command")]
        public static void Command([NotNull] this BssCfg604 server, [NotNull] string command, params object[] args)
        {
            if (server == null) throw new ArgumentNullException("server");
            if (command == null) throw new ArgumentNullException("command");

            var formattedCommand = string.Format(command, args);
            server.ProcessInstruction(formattedCommand);
        }

        /// <summary>
        /// Adds retry/timeout ability to the LockServerSettings method.
        /// </summary>
        public static void LockServerSettings([NotNull] this BssCfg604 server, TimeSpan? timeout)
        {
            if (server == null) throw new ArgumentNullException("server");

            if (!timeout.HasValue)
            {
                server.LockServerSettings();
                return;
            }

            try
            {
                Attempt.Repeatedly.Do(server.LockServerSettings)
                   .CatchWhere(x => x.Exception is COMException)
                   .TakeForDuration(timeout.Value)
                   .DelayWhereFailed(TimeSpan.FromMilliseconds(250))
                   .ThrowIfCantSucceed();
            }
            catch (RepeatedFailureException ex)
            {
                throw new BitviseServerSettingsLockingException("Could not obtain server settings lock.", ex);
            }
        }
    }
}