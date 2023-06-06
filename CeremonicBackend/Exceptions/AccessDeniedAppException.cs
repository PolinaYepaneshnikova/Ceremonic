using System;

namespace CeremonicBackend.Exceptions
{
    public class AccessDeniedAppException : ApplicationException
    {
        public AccessDeniedAppException(string message = null, Exception exception = null) : base(message, exception) { }
    }
}
