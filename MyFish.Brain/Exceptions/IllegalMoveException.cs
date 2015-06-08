namespace MyFish.Brain.Exceptions
{
    public class IllegalMoveException : ClientFaultException
    {
        public IllegalMoveException(string message)
            : base(message)
        {
        }
    }
}