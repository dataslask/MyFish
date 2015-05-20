namespace MyFish.Brain.Pieces
{
    public class Pawn : Piece
    {
        public Pawn(Position position, Color color)
            : base(position, color)
        {
        }

        public override char Type
        {
            get { return 'p'; }
        }
    }
}