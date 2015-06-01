using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace MyFish.Brain.Moves
{
    public class StepperMoves<T> : MovesEnumerator<T>, IEnumerable<Position> where T : Piece
    {
        private readonly List<Vector> _steps;
        private readonly IEnumerator<Vector> _step;

        public StepperMoves(Position position, Board board, params Vector[] steps)
            : this(position, board, steps.ToList())
        {
        }

        private StepperMoves(Position position, Board board, List<Vector> steps)
            : base(position, board)
        {
            _steps = steps;
            _step = _steps.GetEnumerator();
        }

        private StepperMoves(StepperMoves<T> other)
            : this(other.StartingPosition, other.Board, other._steps)
        {
        }

        public override bool MoveNext()
        {
            while (_step.MoveNext())
            {
                Current = StartingPosition + _step.Current;

                if (Current.IsValid && !AtFriendly())
                {
                    return true;
                }
            }
            Current = Position.Invalid;

            return false;
        }

        public IEnumerator<Position> GetEnumerator()
        {
            return new StepperMoves<T>(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}