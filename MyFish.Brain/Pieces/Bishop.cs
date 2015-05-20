namespace MyFish.Brain.Pieces
{
    public class Bishop : Piece
    {
        public Bishop(Position position, Color color)
            : base(position, color)
        {
        }

        public override char Type
        {
            get { return 'b'; }
        }
    }
}