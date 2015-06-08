using System;
using System.Collections.Generic;
using MyFish.Brain.Pieces;

namespace MyFish.Brain
{
    public static class PieceFacory
    {
        private static readonly Dictionary<char, Func<Position, Color, Piece>> Factory = new Dictionary<char, Func<Position, Color, Piece>>
        {
            {'r', (position, color) => new Rook(position, color)},
            {'n', (position, color) => new Knight(position, color)},
            {'b', (position, color) => new Bishop(position, color)},
            {'q', (position, color) => new Queen(position, color)},
            {'k', (position, color) => new King(position, color)},
            {'p', (position, color) => new Pawn(position, color)}
        };

        public static Piece Create(string encodedPiece)
        {
            return Create(encodedPiece[0], encodedPiece.Substring(1));            
        }

        public static Piece Create(char coloredType, Position position)
        {
            var color = coloredType < 'a' ? Color.White : Color.Black;

            var type = color == Color.White ? char.ToLower(coloredType) : coloredType;

            return Factory[type](position, color);
        }

    }
}