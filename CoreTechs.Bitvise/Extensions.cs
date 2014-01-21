﻿using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;
using CoreTechs.Bitvise.Common;
using JetBrains.Annotations;
using BssCfg = BssCfg603Lib.BssCfg603;

namespace CoreTechs.Bitvise
{
    public static class Extensions
    {
        /// <summary>
        /// Like the QueryValue method, but with string format arguments.
        /// </summary>
        [StringFormatMethod("query")]
        public static string Query([NotNull] this BssCfg server, [NotNull] string query, params object[] args)
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
        public static void Command([NotNull] this BssCfg server, [NotNull] string command, params object[] args)
        {
            if (server == null) throw new ArgumentNullException("server");
            if (command == null) throw new ArgumentNullException("command");

            var formattedCommand = string.Format(command, args);
            server.ProcessInstruction(formattedCommand);
        }

        /// <summary>
        /// Adds retry/timeout ability to the LockServerSettings method.
        /// </summary>
        public static void LockServerSettings([NotNull] this BssCfg server, TimeSpan? timeout)
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