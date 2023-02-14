using System;

namespace CeremonicBackend.Exceptions
{
    public class NotFoundAppException : ApplicationException
    {
        public NotFoundAppException(string message = null, Exception exception = null) : base(message, exception) { }
    }
}
