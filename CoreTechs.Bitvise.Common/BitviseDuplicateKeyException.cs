using System;
using System.Runtime.Serialization;

namespace CoreTechs.Bitvise.Common
{
    [Serializable]
    public class BitviseDuplicateKeyException : BitviseSSHServerException
    {
        public BitviseDuplicateKeyException()
        {
        }

        public BitviseDuplicateKeyException(string message) : base(message)
        {
        }

        public BitviseDuplicateKeyException(string message, Exception inner) : base(message, inner)
        {
        }

        protected BitviseDuplicateKeyException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}