using System;
using System.Collections;
using System.Collections.Generic;
using MyFish.Brain.Exceptions;

namespace MyFish.Brain.Moves
{
    public abstract class MovesEnumerator<T> : IEnumerator<Move> where T : Piece
    {
        protected readonly Position StartingPosition;
        protected readonly Board Board;
        protected readonly Color FriendlyColor;
        protected readonly T Piece;

        protected MovesEnumerator(Position position, Board board)
        {
            StartingPosition = position;
            Board = board;

            var piece = Board[position];

            if (piece == null)
            {
                throw new PieceNotFoundException(string.Format("No piece at {0}", position));
            }

            Piece = piece.As<T>();

            FriendlyColor = Piece.Color;
        }

        public void Dispose()
        {
        }

        public abstract bool MoveNext();

        public void Reset()
        {
            throw new NotImplementedException();
        }

        public Move Current { get; protected set; }

        object IEnumerator.Current
        {
            get { return Current; }
        }

        protected bool AtFriendly()
        {
            var piece = Board[Current.Destination];

            return piece != null && piece.Color == FriendlyColor;
        }

        protected bool AtOpponent()
        {
            var piece = Board[Current.Destination];

            return piece != null && piece.Color != FriendlyColor;
        }

        protected bool OpponentAt(Position position)
        {
            var piece = Board[position];

            return piece != null && piece.Color != FriendlyColor;
        }
    }
}