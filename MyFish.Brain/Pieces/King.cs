namespace MyFish.Brain.Pieces
{
    public class King : Piece
    {
        public King(Position position, Color color)
            : base(position, color)
        {
        }

        public override char Type
        {
            get { return 'k'; }
        }
    }
}