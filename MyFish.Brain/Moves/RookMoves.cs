﻿using MyFish.Brain.Pieces;

namespace MyFish.Brain.Moves
{
    public class RookMoves : VectorEnumerator
    {
        public RookMoves(Position position, Board board) : base(position, board, Vector.North, Vector.South, Vector.West, Vector.East)
        {
            Piece.As<Rook>();
        }
    }
}