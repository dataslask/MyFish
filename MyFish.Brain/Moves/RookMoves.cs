using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using MyFish.Brain.Pieces;

namespace MyFish.Brain.Moves
{
    public class RookMoves : IEnumerable<Position>
    {
        private readonly Position _startingPosition;
        private readonly Board _board;
        private readonly Rook _rook;

        public RookMoves(Position position, Board board)
        {
            _startingPosition = position;
            _board = board;
            
            var piece = _board[position];

            if (piece == null)
            {
                throw new ArgumentException(string.Format("No piece at {0}", position));
            }
            _rook = piece.As<Rook>();
        }

        public IEnumerator<Position> GetEnumerator()
        {
            var north = new VectorEnumerator(_startingPosition, Vector.North, _board, _rook.Color);
            var south = new VectorEnumerator(_startingPosition, Vector.South, _board, _rook.Color);
            var west = new VectorEnumerator(_startingPosition, Vector.West, _board, _rook.Color);
            var east = new VectorEnumerator(_startingPosition, Vector.East, _board, _rook.Color);

            return north.Concat(south).Concat(west).Concat(east).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}