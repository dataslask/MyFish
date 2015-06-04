using System;
using System.Collections.Generic;
using MyFish.Brain.Pieces;

namespace MyFish.Brain.Moves
{
    public class PawnMoves : StepperMoves<Pawn>
    {
        private readonly bool _attacksOnly;

        public PawnMoves(Position position, Board board, bool attacksOnly = false)
            : base(position, board)
        {
            _attacksOnly = attacksOnly;
            if (position.Rank == 1 || position.Rank == 8)
            {
                throw new ArgumentException(string.Format("{0} pawn cannot be at at {1}", FriendlyColor, position));                
            }
        }

        protected override IEnumerable<Vector> CalculateSteps()
        {
            var direction = FriendlyColor == Color.White ? 1 : -1;

            if (!_attacksOnly)
            {
                if ((FriendlyColor == Color.White && StartingPosition.Rank == 2 && !PieceAt(0, direction) &&
                     !PieceAt(0, direction*2)) ||
                    FriendlyColor == Color.Black && StartingPosition.Rank == 7 && !PieceAt(0, direction) &&
                    !PieceAt(0, direction*2))
                {
                    yield return new Vector(0, direction*2);
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
            var piece = Board[StartingPosition + new Vector(x, y)];

            return piece != null && piece.Color != FriendlyColor;
        }

        public override IEnumerator<Position> GetEnumerator()
        {
            return new PawnMoves(StartingPosition, Board, _attacksOnly);
        }
    }
}