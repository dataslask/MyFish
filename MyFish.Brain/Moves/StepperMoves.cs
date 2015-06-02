using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace MyFish.Brain.Moves
{
    public abstract class StepperMoves<T> : MovesEnumerator<T>, IEnumerable<Position> where T : Piece
    {
        private List<Vector> _steps;
        private IEnumerator<Vector> _step;

        protected StepperMoves(Position position, Board board)
            : base(position, board)
        {
        }

        public override bool MoveNext()
        {
            if (_steps == null)
            {
                _steps = CalculateSteps().ToList();
                _step = _steps.GetEnumerator();
            }
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

        protected abstract IEnumerable<Vector> CalculateSteps();

        public abstract IEnumerator<Position> GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}