﻿using System;

namespace MyFish.Brain
{
    public abstract class Piece
    {
        public Position Position { get; private set; }
        public Color Color { get; private set; }

        public Piece(Position position, Color color)
        {
            Position = position;
            Color = color;
        }

        public abstract char Type { get; }
        public char ColoredType { get { return Color == Color.White ? char.ToUpper(Type) : Type; } }

        public override string ToString()
        {
            return string.Format("{0}{1}", ColoredType, Position);
        }

        public T As<T>() where T : Piece
        {
            Assert<T>();

            return (T)this;
        }

        public void Assert<T>() where T : Piece
        {
            if (!(this is T))
            {
                throw new InvalidCastException(string.Format("{0} is not a {1}", this, typeof(T).Name));
            }
        }
    }


}