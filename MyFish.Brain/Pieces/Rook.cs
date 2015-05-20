namespace MyFish.Brain.Pieces
{
    public class Rook : Piece
    {
        public Rook(char file, int rank, Color color)
            : base(file, rank, color)
        {
        }

        public override char Type
        {
            get { return 'r'; }
        }
    }
}