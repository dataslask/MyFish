using System.Collections.Generic;
using MyFish.Brain.Pieces;

namespace MyFish.Brain.Moves
{
    public class KingMoves : StepperMoves<King>
    {
        private static readonly List<Vector> Steps = new List<Vector>
        {
            new Vector(1, 0), new Vector(1, 1), new Vector(0, 1), new Vector(-1, 1),
            new Vector(-1, 0), new Vector(-1, -1), new Vector(0, -1), new Vector(1, -1)
        };

        public KingMoves(Position position, Board board, bool avoidCheck = true)
            : base(position, board, avoidCheck)
        {
        }

        protected override IEnumerable<Vector> CalculateSteps()
        {           
            return Steps;
        }

        public override IEnumerator<Move> GetEnumerator()
        {
            return new KingMoves(StartingPosition, Board, AvoidCheck);
        }
    }
}