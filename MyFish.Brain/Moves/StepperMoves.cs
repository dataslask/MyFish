using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace MyFish.Brain.Moves
{
    public abstract class StepperMoves<T> : MovesEnumerator<T>, IEnumerable<Move> where T : Piece
    {
        private List<Vector> _steps;
        private IEnumerator<Vector> _step;

        protected StepperMoves(Position position, Board board, bool avoidCheck)
            : base(position, board, avoidCheck)
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
                var destination = StartingPosition + _step.Current;
                
                Current = new Move(Piece, destination, OpponentAt(destination));

                if (Current.Destination.IsValid && !AtFriendly() && !(AvoidCheck && UnderCheck()))
                {
                    return true;
                }
            }
            Current = Move.Invalid;

            return false;
        }

        protected abstract IEnumerable<Vector> CalculateSteps();

        public abstract IEnumerator<Move> GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}