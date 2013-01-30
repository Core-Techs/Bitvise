using System;
using System.Runtime.Serialization;

namespace CoreTechs.Bitvise
{
    [Serializable]
    public class UnsupportedKeyFormat : BitviseSSHServerException
    {
        public UnsupportedKeyFormat()
        {
        }

        public UnsupportedKeyFormat(string message) : base(message)
        {
        }

        public UnsupportedKeyFormat(string message, Exception inner)
            : base(message, inner)
        {
        }

        protected UnsupportedKeyFormat(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}