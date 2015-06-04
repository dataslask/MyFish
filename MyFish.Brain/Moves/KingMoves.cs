using System.Collections.Generic;
using System.Linq;
using MyFish.Brain.Pieces;

namespace MyFish.Brain.Moves
{
    public class KingMoves : StepperMoves<King>
    {
        private readonly bool _avoidCheck;

        private static readonly List<Vector> Steps = new List<Vector>
        {
            new Vector(1, 0), new Vector(1, 1), new Vector(0, 1), new Vector(-1, 1),
            new Vector(-1, 0), new Vector(-1, -1), new Vector(0, -1), new Vector(1, -1)
        };

        public KingMoves(Position position, Board board, bool avoidCheck = true)
            : base(position, board)
        {
            _avoidCheck = avoidCheck;
        }

        protected override IEnumerable<Vector> CalculateSteps()
        {
            if (_avoidCheck)
            {
                var opponentPieces = Board.Pieces.Where(x => x.Color != FriendlyColor).ToArray();

                var opponentMoves = opponentPieces.ToDictionary(x => x, x => Moves.For(x, Board, true));

                var allOpponentMoves = opponentMoves.SelectMany(x => x.Value).ToArray();

                var distinctOpponentMoves = allOpponentMoves.Distinct().ToArray();

                var opponentSteps = distinctOpponentMoves.Select(x => x - StartingPosition).ToArray();

                return Steps.Except(opponentSteps);
            }
            return Steps;
        }

        public override IEnumerator<Position> GetEnumerator()
        {
            return new KingMoves(StartingPosition, Board, _avoidCheck);
        }
    }
}