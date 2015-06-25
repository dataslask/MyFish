using System.Linq;
using MyFish.Brain.Moves;

namespace MyFish.Brain.Analyzers
{
    public static class MobilityAnalyzer
    {
        public static double MobilityScore(this Board board)
        {
            var whiteMoves = board.MovesFor(Color.White).Count();
            var blackMoves = board.MovesFor(Color.Black).Count();

            return 0.1 * (whiteMoves - blackMoves);
        }
    }
}