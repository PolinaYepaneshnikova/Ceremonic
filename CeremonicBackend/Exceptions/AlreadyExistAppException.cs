using System;

namespace CeremonicBackend.Exceptions
{
    public class AlreadyExistAppException : ApplicationException
    {
        public AlreadyExistAppException(string message = null, Exception exception = null) : base(message, exception) { }
    }
}
