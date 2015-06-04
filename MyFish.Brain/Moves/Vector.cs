namespace MyFish.Brain.Moves
{
    public class Vector
    {
        public static readonly Vector Invalid = new Vector(int.MaxValue, int.MinValue);

        public static readonly Vector North = new Vector(0, 1);
        public static readonly Vector NorthEast = new Vector(1, 1);
        public static readonly Vector East = new Vector(1, 0);
        public static readonly Vector SouthEast = new Vector(1, -1);
        public static readonly Vector South = new Vector(0, -1);
        public static readonly Vector SouthWest = new Vector(-1, -1);
        public static readonly Vector West = new Vector(-1, 0);
        public static readonly Vector NorthWest = new Vector(-1, 1);

        public int X { get; private set; }
        public int Y { get; private set; }

        public Vector(int x, int y)
        {
            X = x;
            Y = y;
        }

        public override string ToString()
        {
            return string.Format("[{0}, {1}]", X, Y);
        }

        protected bool Equals(Vector other)
        {
            return X == other.X && Y == other.Y;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Vector) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (X*397) ^ Y;
            }
        }

        public static bool operator ==(Vector left, Vector right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Vector left, Vector right)
        {
            return !Equals(left, right);
        }
    }
}