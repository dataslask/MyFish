using MyFish.Brain.Pieces;

namespace MyFish.Brain.Moves
{
    public class BishopMoves : SliderMoves<Bishop>
    {
        public BishopMoves(Position position, Board board, bool avoidCheck = true)
            : base(position, board, avoidCheck, Vector.NorthEast, Vector.SouthEast, Vector.NorthWest, Vector.SouthWest)
        {
        }
    }
}