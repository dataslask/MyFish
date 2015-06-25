using System.Collections.Generic;
using System.Linq;
using MyFish.Brain;

namespace MyFish.Tests.Helpers
{
    public static class Expected
    {
        public static Move Move(string piece, string destination)
        {
            return new Move(PieceFacory.Create(piece), Destination(destination), IsAttack(destination));
        }

        public static IEnumerable<Move> Moves(string piece, string destinations)
        {
            return destinations.Split(' ').Select(x => new Move(PieceFacory.Create(piece), Destination(x), IsAttack(x)));
        }

        private static bool IsAttack(string destination)
        {
            return destination.StartsWith("x");
        }

        private static string Destination(string destination)
        {
            return IsAttack(destination) ? destination.Substring(1) : destination;
        }
    }
}