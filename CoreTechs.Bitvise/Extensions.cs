using BssCfg554Lib;
using JetBrains.Annotations;

namespace CoreTechs.Bitvise
{
    public static class Extensions
    {
        [StringFormatMethod("query")]
        public static string Query(this BssCfg554 bssCfg, string query, params object[] args)
        {
            return bssCfg.QueryValue(string.Format(query, args), 0);
        }

        [StringFormatMethod("command")]
        public static void Command(this BssCfg554 bssCfg, string command, params object[] args)
        {
            bssCfg.ProcessInstruction(string.Format(command, args));
        }
    }
}