using System;
using System.Collections;
using System.Collections.Generic;

namespace MyFish.Brain.Moves
{
    public class VectorEnumerator : MovesEnumerator, IEnumerable<Position>
    {
        private readonly Position _position;
        private readonly Board _board;
        private readonly Vector[] _vectors;
        private readonly Color _friendlyColor;
        protected readonly Piece Piece;

        private int _currentVector = 0;

        public VectorEnumerator(Position position, Board board, params Vector[] vectors)
            : base(position)
        {
            _vectors = vectors;

            _position = position;
            _board = board;
            Piece = _board[position];

            if (Piece == null)
            {
                throw new ArgumentException(string.Format("No piece at {0}", position));
            }

            _friendlyColor = Piece.Color;
        }

        private VectorEnumerator(VectorEnumerator other)
            : this(other._position, other._board, other._vectors)
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
                    return TryNextVector();
                }
            }
            Current += _vectors[_currentVector];

            if (AtFriendly() || !Current.IsValid)
            {
                return TryNextVector();
            }
            return Current.IsValid;
        }

        private bool TryNextVector()
        {
            if (++_currentVector == _vectors.Length)
            {
                Current = Position.Invalid;
                return false;
            }
            Current = StartingPosition;

            BeforeStart = false;
            
            return MoveNext();
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