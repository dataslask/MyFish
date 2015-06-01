using System;
using System.Collections;
using System.Collections.Generic;

namespace MyFish.Brain.Moves
{
    public abstract class MovesEnumerator<T> : IEnumerator<Position> where T : Piece
    {
        protected readonly Position StartingPosition;
        protected readonly Board Board;
        protected readonly Color FriendlyColor;

        protected MovesEnumerator(Position position, Board board)
        {
            StartingPosition = position;
            Board = board;

            var piece = Board[position];

            if (piece == null)
            {
                throw new ArgumentException(string.Format("No piece at {0}", position));
            }

            piece.AssertIs<T>();

            FriendlyColor = piece.Color;
        }

        public void Dispose()
        {
        }

        public abstract bool MoveNext();

        public void Reset()
        {
            throw new NotImplementedException();
        }

        public Position Current { get; protected set; }

        object IEnumerator.Current
        {
            get { return Current; }
        }

        protected bool AtFriendly()
        {
            var piece = Board[Current];

            return piece != null && piece.Color == FriendlyColor;
        }

        protected bool AtOpponent()
        {
            var piece = Board[Current];

            return piece != null && piece.Color != FriendlyColor;
        }
    }
}