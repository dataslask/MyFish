namespace MyFish.Brain.Pieces
{
    public class Pawn : Piece
    {
        public Pawn(char file, int rank, Color color)
            : base(file, rank, color)
        {
        }

        public override char Type
        {
            get { return 'p'; }
        }
    }
}