using System;

namespace MyFish.Brain.Exceptions
{
    public abstract class ClientFaultException : Exception
    {
        protected ClientFaultException(string message) : base(message)
        {
        }
    }
}