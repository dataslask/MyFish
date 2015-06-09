namespace MyFish.Web.Contracts.Commands
{
    public class MoveCommand
    {
        public Piece Piece { get; set; }
        public Position Destination { get; set; }
    }
}