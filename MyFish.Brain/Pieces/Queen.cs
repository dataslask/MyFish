namespace MyFish.Brain.Pieces
{
    public class Queen : Piece
    {
        public Queen(Position position, Color color)
            : base(position, color)
        {
        }

        public override char Type
        {
            get { return 'q'; }
        }
    }
}