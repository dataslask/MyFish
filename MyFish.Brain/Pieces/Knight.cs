namespace MyFish.Brain.Pieces
{
    public class Knight : Piece
    {
        public Knight(Position position, Color color)
            : base(position, color)
        {
        }

        public override char Type
        {
            get { return 'n'; }
        }
    }
}