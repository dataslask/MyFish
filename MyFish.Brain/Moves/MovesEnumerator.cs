using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using MyFish.Brain.Exceptions;

namespace MyFish.Brain.Moves
{
    public abstract class MovesEnumerator<T> : IEnumerator<Move> where T : Piece
    {
        protected readonly Position StartingPosition;
        protected readonly Board Board;
        protected readonly bool AvoidCheck;
        protected readonly Color FriendlyColor;
        protected readonly T Piece;

        protected Color OpponentColor { get { return FriendlyColor == Color.White ? Color.Black : Color.White; } }

        protected MovesEnumerator(Position position, Board board, bool avoidCheck)
        {
            StartingPosition = position;
            Board = board;
            AvoidCheck = avoidCheck;

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

        protected bool UnderCheck()
        {
            var board = Board.Do(Current);

            var king = board.KingOf(FriendlyColor);

            var positions = board.PositionsCoveredBy(OpponentColor);

            return positions.Contains(king.Position);
        }
    }
}