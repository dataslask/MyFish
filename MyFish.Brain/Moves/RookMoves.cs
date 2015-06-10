using MyFish.Brain.Pieces;

namespace MyFish.Brain.Moves
{
    public class RookMoves : SliderMoves<Rook>
    {
        public RookMoves(Position position, Board board, bool avoidCheck = true)
            : base(position, board, avoidCheck, Vector.North, Vector.South, Vector.West, Vector.East)
        {
        }
    }
}