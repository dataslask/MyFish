using System.Collections.Generic;
using System.Linq;
using MyFish.Brain;

namespace MyFish.Tests.Helpers
{
    public static class MovesAttacsExtension
    {
        public static IEnumerable<Move> Attacks(this IEnumerable<Move> moves)
        {
            return moves.Where(x => x.IsAttack);
        }
    }
}