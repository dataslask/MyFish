using System.Collections.Generic;
using System.Linq;

namespace MyFish.Brain.Analyzers
{
    public static class PieceValueAnalyzer
    {
        private static readonly Dictionary<char, int> PieceValues = new Dictionary<char, int>()
        {
            {'p', 1}, {'r', 5}, {'b', 3}, {'n', 3}, {'q', 9}, {'k', 1000}
        };

        public static double PieceScore(this Board board)
        {
            return board.WhitePieces.Sum(x => PieceValues[x.Type]) - board.BlackPieces.Sum(x => PieceValues[x.Type]);
        }
    }
}