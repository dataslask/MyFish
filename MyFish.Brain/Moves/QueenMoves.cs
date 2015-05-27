using MyFish.Brain.Pieces;

namespace MyFish.Brain.Moves
{
    public class QueenMoves : SliderMoves
    {
        public QueenMoves(Position position, Board board)
            : base(position, board, Vector.North, Vector.South, Vector.West, Vector.East, Vector.NorthEast, Vector.SouthEast, Vector.NorthWest, Vector.SouthWest)
        {
            Piece.AssertIs<Queen>();
        }
    }
}