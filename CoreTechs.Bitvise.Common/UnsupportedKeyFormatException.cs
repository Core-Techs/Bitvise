using System;
using System.Runtime.Serialization;

namespace CoreTechs.Bitvise.Common
{
    [Serializable]
    public class UnsupportedKeyFormatException : BitviseSSHServerException
    {
        public UnsupportedKeyFormatException()
        {
        }

        public UnsupportedKeyFormatException(string message) : base(message)
        {
        }

        public UnsupportedKeyFormatException(string message, Exception inner)
            : base(message, inner)
        {
        }

        protected UnsupportedKeyFormatException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}