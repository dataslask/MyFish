namespace MyFish.Brain.Exceptions
{
    public class WrongTurnException : ClientFaultException
    {
        public WrongTurnException(string message)
            : base(message)
        {
        }
    }
}