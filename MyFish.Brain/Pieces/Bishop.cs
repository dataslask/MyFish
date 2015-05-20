namespace MyFish.Brain.Pieces
{
    public class Bishop : Piece
    {
        public Bishop(char file, int rank, Color color)
            : base(file, rank, color)
        {
        }

        public override char Type
        {
            get { return 'b'; }
        }
    }
}