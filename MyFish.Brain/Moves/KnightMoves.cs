using MyFish.Brain.Pieces;

namespace MyFish.Brain.Moves
{
    public class KnightMoves : StepperMoves<Knight>
    {
        private static readonly Vector[] Steps =
        {
            new Vector(2, 1), new Vector(2, -1), new Vector(-2, 1), new Vector(-2, -1),
            new Vector(1, 2), new Vector(-1, 2), new Vector(1, -2), new Vector(-1, -2)
        };

        public KnightMoves(Position position, Board board)
            : base(position, board, Steps)
        {
        }
    }
}