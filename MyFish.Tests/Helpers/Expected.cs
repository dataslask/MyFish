using System.Collections.Generic;
using System.Linq;
using MyFish.Brain;

namespace MyFish.Tests.Helpers
{
    public static class Expected
    {
        public static IEnumerable<Move> Moves(string piece, string positions)
        {
            return positions.Split(' ').Select(x => new Move(CreatePiece(piece), x));
        }

        private static Piece CreatePiece(string encodedPiece)
        {
            return PieceFacory.Create(encodedPiece[0], encodedPiece.Substring(1));
        }

    }
}