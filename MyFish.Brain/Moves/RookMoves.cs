using MyFish.Brain.Pieces;

namespace MyFish.Brain.Moves
{
    public class RookMoves : SliderMoves
    {
        public RookMoves(Position position, Board board)
            : base(position, board, Vector.North, Vector.South, Vector.West, Vector.East)
        {
            Piece.AssertIs<Rook>();
        }
    }
}