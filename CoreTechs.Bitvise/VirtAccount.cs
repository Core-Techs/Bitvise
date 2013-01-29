using System.Text.RegularExpressions;

namespace CoreTechs.Bitvise
{
    public class VirtAccount
    {
        public string AccountData { get; private set; }
        public string KeyData { get; set; }
        public string Id { get; private set; }
        public string Group { get; private set; }
        public string Username { get; private set; }

        public VirtAccount(string id, string accountData, string keyData)
        {
            Id = id;
            AccountData = accountData;
            KeyData = keyData;
            Username = Regex.Match(accountData, @"^virtAccount ""(.+)""$", RegexOptions.Multiline).Groups[1].Value;
            Group = Regex.Match(accountData, @"^group ""(.+)""$", RegexOptions.Multiline).Groups[1].Value;
        }
    }
}