namespace MyFish.Brain.Exceptions
{
    public class MissingKingException : ClientFaultException
    {
        public MissingKingException(string message)
            : base(message)
        {
        }
    }
}