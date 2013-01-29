using System;
using System.Runtime.Serialization;

namespace CoreTechs.Bitvise
{
    [Serializable]
    public class BitviseServerSettingsLockingException : BitviseSSHServerException
    {
        public BitviseServerSettingsLockingException()
        {
        }

        public BitviseServerSettingsLockingException(string message) : base(message)
        {
        }

        public BitviseServerSettingsLockingException(string message, Exception inner) : base(message, inner)
        {
        }

        protected BitviseServerSettingsLockingException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}