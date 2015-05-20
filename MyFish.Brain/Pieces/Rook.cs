namespace MyFish.Brain.Pieces
{
    public class Rook : Piece
    {
        public Rook(Position position, Color color)
            : base(position, color)
        {
        }

        public override char Type
        {
            get { return 'r'; }
        }
    }
}