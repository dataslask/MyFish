using System.Collections.Generic;
using System.Linq;
using MyFish.Brain;

namespace MyFish.Tests.Helpers
{
    public static class MovesJoinExtension
    {
        public static string Join(this IEnumerable<Move> moves, string separator = " ")
        {
            return string.Join(separator, moves.Select(x => x.Destination));
        }

    }
}