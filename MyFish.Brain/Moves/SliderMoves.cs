using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace MyFish.Brain.Moves
{
    public class SliderMoves<T> : MovesEnumerator<T>, IEnumerable<Move> where T : Piece
    {
        private readonly List<Vector> _vectors;
        private readonly IEnumerator<Vector> _vector;

        private bool _beforeStart;

        public SliderMoves(Position position, Board board, bool avoidCheck, params Vector[] vectors)
            : this(position, board, avoidCheck, vectors.ToList())
        {
        }

        private SliderMoves(Position position, Board board, bool avoidCheck, List<Vector> vectors)
            : base(position, board, avoidCheck)
        {
            _beforeStart = true;

            _vectors = vectors;

            _vector = _vectors.GetEnumerator();
        }

        private SliderMoves(SliderMoves<T> other)
            : this(other.StartingPosition, other.Board, other.AvoidCheck, other._vectors)
        {
        }

        public override bool MoveNext()
        {
            while (TryMoveNext())
            {
                if (Current.Destination.IsValid&& !(AvoidCheck && UnderCheck()))
                {
                    return true;
                }
            }
            Current = Move.Invalid;

            return false;
        }

        private bool TryMoveNext()
        {
            if (_beforeStart || AtOpponent())
            {
                return TryNextVector();
            }

            var destination = Current.Destination + _vector.Current;

            Current = new Move(Piece, destination, OpponentAt(destination));

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
                Current = new Move(Piece, StartingPosition, false);

                _beforeStart = false;

                return MoveNext();
            }

            Current = Move.Invalid;

            return false;
        }

        public IEnumerator<Move> GetEnumerator()
        {
            return new SliderMoves<T>(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}