using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using MyFish.Brain.Pieces;

namespace MyFish.Brain.Moves
{
    public class BishopMoves : IEnumerable<Position>
    {
        private readonly Position _startingPosition;
        private readonly Board _board;
        private readonly Bishop _bishop;

        public BishopMoves(Position position, Board board)
        {
            _startingPosition = position;
            _board = board;
            
            var piece = _board[position];

            if (piece == null)
            {
                throw new ArgumentException(string.Format("No piece at {0}", position));
            }
            _bishop = piece.As<Bishop>();
        }

        public IEnumerator<Position> GetEnumerator()
        {
            var northEast = new VectorEnumerator(_startingPosition, Vector.NorthEast, _board, _bishop.Color);
            var southEast = new VectorEnumerator(_startingPosition, Vector.SouthEast, _board, _bishop.Color);
            var northWest = new VectorEnumerator(_startingPosition, Vector.NorthWest, _board, _bishop.Color);
            var southWest = new VectorEnumerator(_startingPosition, Vector.SouthWest, _board, _bishop.Color);

            return northEast.Concat(southEast).Concat(northWest).Concat(southWest).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}