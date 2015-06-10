using MyFish.Brain.Pieces;

namespace MyFish.Brain.Moves
{
    public class QueenMoves : SliderMoves<Queen>
    {
        public QueenMoves(Position position, Board board, bool avoidCheck = true)
            : base(position, board, avoidCheck, Vector.North, Vector.South, Vector.West, Vector.East, Vector.NorthEast, Vector.SouthEast, Vector.NorthWest, Vector.SouthWest)
        {
        }
    }
}