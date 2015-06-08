namespace MyFish.Brain.Exceptions
{
    public class PieceNotFoundException : ClientFaultException
    {
        public PieceNotFoundException(string message)
            : base(message)
        {
        }
    }
}