namespace MyFish.Brain.Pieces
{
    public class King : Piece
    {
        public King(char file, int rank, Color color)
            : base(file, rank, color)
        {
        }

        public override char Type
        {
            get { return 'k'; }
        }
    }
}