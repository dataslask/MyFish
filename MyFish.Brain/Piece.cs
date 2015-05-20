using System;
using System.Collections.Generic;
using MyFish.Brain.Pieces;

namespace MyFish.Brain
{
    public abstract class Piece
    {
        public int Rank { get; private set; }

        public char File { get; private set; }

        public Color Color { get; set; }

        public Piece(char file, int rank, Color color)
        {
            Rank = rank;
            File = file;
            Color = color;

            AssertValid();
        }

        public abstract char Type { get; }
        public char ColoredType { get { return Color == Color.White ? char.ToUpper(Type) : Type; } }

        public string Position
        {
            get { return string.Format("{0}{1}", File, Rank); }
        }

        private void AssertValid()
        {
            if (Rank < 1 || Rank > 8) throw new InvalidOperationException("Rank must be between 1 and 8");
            if (File < 'a' || File > 'h') throw new InvalidOperationException("File must be between a and h");
        }

        public override string ToString()
        {
            return string.Format("{0}{1}", ColoredType, Position);
        }

        protected static readonly Dictionary<char, Func<char, int, Color, Piece>> Factory = new Dictionary<char, Func<char, int, Color, Piece>>
        {
            {'r', (file, rank, color) => new Rook(file, rank, color)},
            {'n', (file, rank, color) => new Knight(file, rank, color)},
            {'b', (file, rank, color) => new Bishop(file, rank, color)},
            {'q', (file, rank, color) => new Queen(file, rank, color)},
            {'k', (file, rank, color) => new King(file, rank, color)},
            {'p', (file, rank, color) => new Pawn(file, rank, color)}
        };

        public static Piece Create(char coloredType, char file, int rank)
        {
            var color = coloredType < 'a' ? Color.White : Color.Black;

            var type = color == Color.White ? char.ToLower(coloredType) : coloredType;

            if (!Factory.ContainsKey(type))
            {
                throw new ArgumentOutOfRangeException("coloredType", string.Format("Don't know how to create pieces of type '{0}'", type));
            }
            return Factory[type](file, rank, color);
        }
    }
}