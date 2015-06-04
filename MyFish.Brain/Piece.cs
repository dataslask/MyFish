using System;

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
            AssertIs<T>();

            return (T)this;
        }

        public void AssertIs<T>() where T : Piece
        {
            if (!(this is T))
            {
                throw new InvalidCastException(string.Format("{0} is not a {1}", this, typeof(T).Name));
            }
        }

        protected bool Equals(Piece other)
        {
            return Equals(Position, other.Position) && Color == other.Color;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Piece) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((Position != null ? Position.GetHashCode() : 0)*397) ^ (int) Color;
            }
        }

        public static bool operator ==(Piece left, Piece right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Piece left, Piece right)
        {
            return !Equals(left, right);
        }
    }


}