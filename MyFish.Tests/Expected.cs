using System.Collections.Generic;
using System.Linq;
using MyFish.Brain;

namespace MyFish.Tests
{
    public static class Expected
    {
        public static IEnumerable<Position> Moves(string positions)
        {
            return positions.Split(' ').Select(x => (Position)x);
        }
    }
}