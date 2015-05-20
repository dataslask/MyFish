namespace MyFish.Brain.Moves
{
    public class Vector
    {
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

        private Vector(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}