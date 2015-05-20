using System.Collections;
using System.Collections.Generic;

namespace MyFish.Brain.Moves
{
    public class VectorEnumerator : MovesEnumerator, IEnumerable<Position>
    {
        private readonly Vector _vector;
        private readonly Board _board;
        private readonly Color _friendlyColor;

        public VectorEnumerator(Position position, Vector vector, Board board, Color friendlyColor)
            : base(position)
        {
            _vector = vector;
            _board = board;
            _friendlyColor = friendlyColor;
        }

        private VectorEnumerator(VectorEnumerator other)
            : this(other.StartingPosition, other._vector, other._board, other._friendlyColor)
        {
        }

        public override bool MoveNext()
        {
            if (BeforeStart)
            {
                Current = StartingPosition;

                BeforeStart = false;
            }
            else
            {
                if (AtOpponent())
                {
                    return Stop();
                }
            }
            Current += _vector;

            if (AtFriendly())
            {
                return Stop();
            }
            return Current.IsValid;
        }

        private bool Stop()
        {
            Current = Position.Invalid;

            return false;
        }

        private bool AtFriendly()
        {
            var piece = _board[Current];

            return piece != null && piece.Color == _friendlyColor;
        }

        private bool AtOpponent()
        {
            var piece = _board[Current];

            return piece != null && piece.Color != _friendlyColor;
        }

        public IEnumerator<Position> GetEnumerator()
        {
            return new VectorEnumerator(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}