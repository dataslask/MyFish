namespace MyFish.Brain.Pieces
{
    public class Queen : Piece
    {
        public Queen(char file, int rank, Color color)
            : base(file, rank, color)
        {
        }

        public override char Type
        {
            get { return 'q'; }
        }
    }
}