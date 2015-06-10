using System.Collections.Generic;
using MyFish.Brain.Pieces;

namespace MyFish.Brain.Moves
{
    public class KnightMoves : StepperMoves<Knight>
    {
        private static readonly List<Vector> Steps = new List<Vector>
        {
            new Vector(2, 1), new Vector(2, -1), new Vector(-2, 1), new Vector(-2, -1),
            new Vector(1, 2), new Vector(-1, 2), new Vector(1, -2), new Vector(-1, -2)
        };

        public KnightMoves(Position position, Board board, bool avoidCheck = true)
            : base(position, board, avoidCheck)
        {
        }

        protected override IEnumerable<Vector> CalculateSteps()
        {
            return Steps;
        }

        public override IEnumerator<Move> GetEnumerator()
        {
            return new KnightMoves(StartingPosition, Board, AvoidCheck);
        }
    }
}