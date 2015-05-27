using MyFish.Brain.Pieces;

namespace MyFish.Brain.Moves
{
    public class BishopMoves : SliderMoves<Bishop>
    {
        public BishopMoves(Position position, Board board)
            : base(position, board, Vector.NorthEast, Vector.SouthEast, Vector.NorthWest, Vector.SouthWest)
        {
        }
    }
}