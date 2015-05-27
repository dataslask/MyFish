using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace MyFish.Brain.Moves
{
    public class SliderMoves<T> : MovesEnumerator<T>, IEnumerable<Position> where T : Piece
    {
        private readonly List<Vector> _vectors;
        private readonly IEnumerator<Vector> _vector;

        public SliderMoves(Position position, Board board, params Vector[] vectors)
            : this(position, board, vectors.ToList())
        {
        }

        private SliderMoves(Position position, Board board, List<Vector> vectors)
            : base(position, board)
        {
            _vectors = vectors;

            _vector = _vectors.GetEnumerator();
        }

        private SliderMoves(SliderMoves<T> other)
            : this(other.StartingPosition, other.Board, other._vectors)
        {
        }

        public override bool MoveNext()
        {
            if (BeforeStart || AtOpponent())
            {
                return TryNextVector();
            }

            Current += _vector.Current;

            if (AtFriendly() || !Current.IsValid)
            {
                return TryNextVector();
            }
            return true;
        }

        private bool TryNextVector()
        {
            if (_vector.MoveNext())
            {
                Current = StartingPosition;

                BeforeStart = false;

                return MoveNext();
            }

            Current = Position.Invalid;

            return false;
        }

        public IEnumerator<Position> GetEnumerator()
        {
            return new SliderMoves<T>(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}