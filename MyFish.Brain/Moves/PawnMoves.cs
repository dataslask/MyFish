using System.Collections.Generic;
using MyFish.Brain.Exceptions;
using MyFish.Brain.Pieces;

namespace MyFish.Brain.Moves
{
    public class PawnMoves : StepperMoves<Pawn>
    {
        public PawnMoves(Position position, Board board, bool avoidCheck = true)
            : base(position, board, avoidCheck)
        {
            if (position.Rank == 1 || position.Rank == 8)
            {
                throw new IllegalPawnPositionException(string.Format("{0} pawn cannot be at at {1}", FriendlyColor, position));
            }
        }

        protected override IEnumerable<Vector> CalculateSteps()
        {
            var direction = FriendlyColor == Color.White ? 1 : -1;

            if (AvoidCheck)
            {
                if ((FriendlyColor == Color.White && StartingPosition.Rank == 2 && !PieceAt(0, direction) && !PieceAt(0, direction * 2)) ||
                    FriendlyColor == Color.Black && StartingPosition.Rank == 7 && !PieceAt(0, direction) && !PieceAt(0, direction * 2))
                {
                    yield return new Vector(0, direction * 2);
                }
                if (!PieceAt(0, direction))
                {
                    yield return new Vector(0, direction);
                }
            }
            if (OpponentAt(-1, direction) || EnPassantTargetAt(-1, direction))
            {
                yield return new Vector(-1, direction);
            }
            if (OpponentAt(1, direction) || EnPassantTargetAt(1, direction))
            {
                yield return new Vector(1, direction);
            }
        }

        private bool EnPassantTargetAt(int x, int y)
        {
            return Board.EnPassantTarget == StartingPosition + new Vector(x, y);
        }

        private bool PieceAt(int x, int y)
        {
            return Board[StartingPosition + new Vector(x, y)] != null;
        }

        private bool OpponentAt(int x, int y)
        {
            return OpponentAt(StartingPosition + new Vector(x, y));
        }

        public override IEnumerator<Move> GetEnumerator()
        {
            return new PawnMoves(StartingPosition, Board, AvoidCheck);
        }
    }
}