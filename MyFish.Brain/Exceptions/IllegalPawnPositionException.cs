namespace MyFish.Brain.Exceptions
{
    public class IllegalPawnPositionException : ClientFaultException
    {
        public IllegalPawnPositionException(string message)
            : base(message)
        {
        }
    }
}