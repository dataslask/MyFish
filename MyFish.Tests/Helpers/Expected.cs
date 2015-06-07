using System.Collections.Generic;
using System.Linq;
using MyFish.Brain;

namespace MyFish.Tests.Helpers
{
    public static class Expected
    {
        public static IEnumerable<Move> Moves(string piece, string positions)
        {
            return positions.Split(' ').Select(x => new Move(CreatePiece(piece), Destination(x), IsAttack(x)));
        }

        private static bool IsAttack(string destination)
        {
            return destination.StartsWith("x");
        }

        private static string Destination(string destination)
        {
            return IsAttack(destination) ? destination.Substring(1) : destination;
        }

        private static Piece CreatePiece(string encodedPiece)
        {
            return PieceFacory.Create(encodedPiece[0], encodedPiece.Substring(1));
        }

    }
}