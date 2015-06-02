using System;
using MyFish.Brain.Moves;

namespace MyFish.Brain
{
    public class Position
    {
        public static readonly Position Invalid = new Position();

        public char File { get; private set; }
        public int Rank { get; private set; }

        public Position(char file, int rank) : this(file, rank, true)
        {
        }

        public Position(string position) : this(position[0], int.Parse(position.Substring(1)))
        {
        }

        private Position() : this('x', 9, false)
        {
        }

        public static implicit operator Position(string position)
        {
            return string.IsNullOrEmpty(position) ? Position.Invalid : new Position(position);
        }

        public static Position operator+ (Position position, Vector vector)
        {
            return position.IsValid ? new Position((char) (position.File + vector.X), position.Rank + vector.Y, false) : Invalid;
        }

        private Position(char file, int rank, bool assertValid)
        {
            File = file;
            Rank = rank;

            if (assertValid && !IsValid)
            {
                throw new ArgumentException(string.Format("Invalid position: {0}", this));
            }
        }

        public bool IsValid
        {
            get { return Rank >= 1 && Rank <= 8 && File >= 'a' && File <= 'h'; }
        }

        public override string ToString()
        {
            return IsValid ? string.Format("{0}{1}", File, Rank) : "x9";
        }

        protected bool Equals(Position other)
        {
            return (File == other.File && Rank == other.Rank) || (!IsValid && !other.IsValid);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Position) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (File.GetHashCode()*397) ^ Rank;
            }
        }

        public static bool operator ==(Position left, Position right)
        {
            if (ReferenceEquals(null, left)) return false;

            return left.Equals(right);
        }

        public static bool operator !=(Position left, Position right)
        {
            if (ReferenceEquals(null, left)) return false;

            return !left.Equals(right);
        }
    }
}