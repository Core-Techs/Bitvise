using System;
using System.Runtime.Serialization;

namespace CoreTechs.Bitvise
{
    [Serializable]
    public class BitviseSSHServerException : Exception
    {
        public BitviseSSHServerException()
        {
        }

        public BitviseSSHServerException(string message)
            : base(message)
        {
        }

        public BitviseSSHServerException(string message, Exception inner)
            : base(message, inner)
        {
        }

        protected BitviseSSHServerException(
            SerializationInfo info,
            StreamingContext context)
            : base(info, context)
        {
        }
    }
}