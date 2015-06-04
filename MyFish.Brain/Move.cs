using MyFish.Brain.Moves;

namespace MyFish.Brain
{
    public class Move
    {
        public static readonly Move Invalid = new Move();

        public Piece Piece { get; private set; }
        public Position Destination { get; private set; }

        public bool IsValid { get { return Piece != null && Destination.IsValid; } }

        public Move(Piece piece, Position destination)
        {
            Piece = piece;
            Destination = destination;
        }

        private Move()
        {
            Destination = Position.Invalid;
        }

        public static Move operator +(Move move, Vector vector)
        {
            return move.IsValid ? new Move(move.Piece, move.Destination + vector) : Invalid;
        }

        public static Vector operator -(Move move, Position position)
        {
            return move.IsValid && position.IsValid ? move.Destination - position : Vector.Invalid;
        }

        public override string ToString()
        {
            return IsValid ? string.Format("{0}->{1}", Piece, Destination) : "xx9->x9";
        }

        protected bool Equals(Move other)
        {
            return Equals(Piece, other.Piece) && Equals(Destination, other.Destination);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Move) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((Piece != null ? Piece.GetHashCode() : 0)*397) ^ (Destination != null ? Destination.GetHashCode() : 0);
            }
        }

        public static bool operator ==(Move left, Move right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Move left, Move right)
        {
            return !Equals(left, right);
        }
    }
}