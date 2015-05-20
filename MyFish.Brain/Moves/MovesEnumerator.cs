using System.Collections;
using System.Collections.Generic;

namespace MyFish.Brain.Moves
{
    public abstract class MovesEnumerator : IEnumerator<Position>
    {
        protected readonly Position StartingPosition;
        protected bool BeforeStart;

        public MovesEnumerator(Position position)
        {
            StartingPosition = position;
            
            Reset();
        }

        public void Dispose()
        {
        }

        public abstract bool MoveNext();

        public void Reset()
        {
            BeforeStart = true;
        }

        public Position Current { get; protected set; }

        object IEnumerator.Current
        {
            get { return Current; }
        }
    }
}