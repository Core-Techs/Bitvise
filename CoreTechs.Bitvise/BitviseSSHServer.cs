using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using CoreTechs.Bitvise.Common;
using JetBrains.Annotations;
using BssCfg631Lib;

namespace CoreTechs.Bitvise
{
    public class BitviseSSHServer
    {
        private readonly BssCfg631 _server = new BssCfg631();

        public TimeSpan? SettingsLockTimeout { get; set; }

        public BitviseSSHServer(string site = null)
        {
            if (site != null)
                _server.SetSite(site);
        }

        public IEnumerable<string> GetVirtAccountIds(string query = null)
        {
            _server.LoadServerSettings();
            var hasPredicate = !string.IsNullOrWhiteSpace(query);

            try
            {
                var rawResult = hasPredicate ? _server.Query("access.virtAccounts.({0})", query) : _server.Query("access.virtAccounts.All");
                var results = rawResult.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
                return results;
            }
            catch (COMException ex)
            {
                if (ex.Message.ToLowerInvariant().Contains("no match found"))
                    return new string[0];

                throw;
            }
        }

        public IEnumerable<VirtAccount> GetVirtAccounts(string query = null)
        {
            return from id in GetVirtAccountIds(query)
                   let accountData = _server.Query("{0}.*", id)
                   let keyData = _server.Query("{0}.keys.Info", id)
                   select new VirtAccount(id, accountData, keyData);
        }

        public VirtAccount CreateOrUpdateVirtAccount([NotNull] string username, string password = null,
                                                     string group = null)
        {
            if (username == null) throw new ArgumentNullException("username");

            try
            {
                _server.LockServerSettings(SettingsLockTimeout);
                _server.LoadServerSettings();

                // check for existing user
                var query = string.Format(@"virtAccount eqi ""{0}""", username);
                var exists = int.Parse(_server.Query("access.virtAccounts.Count({0})", query)) > 0;
                var virtAccount = exists
                                      ? GetVirtAccounts(query).SingleOrDefault()
                                      : new VirtAccount("access.virtAccounts.New", "", "");

                if (!exists)
                    _server.Command(@"access.virtAccounts.NewClear");

                _server.Command(@"{0}.virtAccount ""{1}""", virtAccount.Id, username);

                if (!string.IsNullOrWhiteSpace(password))
                    _server.Command(@"{0}.virtPassword.Set ""{1}""", virtAccount.Id, password);

                if (!string.IsNullOrWhiteSpace(group))
                    _server.Command(@"{0}.group ""{1}""", virtAccount.Id, group);

                if (!exists)
                    _server.Command("access.virtAccounts.NewCommit");

                _server.SaveServerSettings();

                virtAccount = GetVirtAccounts(query).Single();
                return virtAccount;
            }
            catch (COMException ex)
            {
                throw new BitviseSSHServerException(ex);
            }
            finally
            {
                _server.UnlockServerSettings();
            }
        }

        public void ImportVirtAccountPublicKey(string username, FileInfo keyFile)
        {
            var keyFilePath = keyFile.FullName.Replace(@"\", @"\\");

            try
            {
                _server.LockServerSettings(SettingsLockTimeout);
                _server.LoadServerSettings();
                try
                {
                    _server.Command(@"access.virtAccounts.(virtAccount eqi ""{0}"").keys.Import ""{1}""", username,
                                    keyFilePath);
                }
                catch (COMException ex)
                {
                    var message = ex.Message.ToLower();

                    if (message.Contains("public key is already in the list"))
                        throw new BitviseDuplicateKeyException("The public key is already in use.", ex);

                    if (message.Contains("import format"))
                        throw new UnsupportedKeyFormatException("The imported key file contains an unsupported format.", ex);

                    throw;
                }
                _server.SaveServerSettings();
            }
            catch (COMException ex)
            {
                throw new BitviseSSHServerException(ex);
            }
            finally
            {
                _server.UnlockServerSettings();
            }
        }
    }
}