namespace MyFish.Brain.Pieces
{
    public class Knight : Piece
    {
        public Knight(char file, int rank, Color color)
            : base(file, rank, color)
        {
        }

        public override char Type
        {
            get { return 'n'; }
        }
    }
}