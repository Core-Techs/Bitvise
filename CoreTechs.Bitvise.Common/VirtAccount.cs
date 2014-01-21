using System.Linq;
using System.Text.RegularExpressions;

namespace CoreTechs.Bitvise.Common
{
    public class VirtAccount
    {
        public string AccountData { get;  set; }
        public string PublicKeyData { get; set; }
        public string Id { get;  set; }
        public string Group { get;  set; }
        public string Username { get;  set; }
        public PublicKeyInfo[] PublicKeys { get;  set; }

        public VirtAccount()
        {
            
        }

        public VirtAccount(string id, string accountData, string publicKeyData)
        {
            Id = id;
            AccountData = accountData;
            PublicKeyData = publicKeyData;
            Username = Regex.Match(accountData, @"virtAccount ""(.+)""", RegexOptions.Multiline).Groups[1].Value;
            Group = Regex.Match(accountData, @"group ""(.+)""", RegexOptions.Multiline).Groups[1].Value;

            PublicKeys = PublicKeyInfo.ParseKeyData(publicKeyData).ToArray();
        }
    }
}