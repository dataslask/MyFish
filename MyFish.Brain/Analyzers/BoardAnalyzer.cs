using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using MyFish.Brain.Moves;

namespace MyFish.Brain.Analyzers
{
    public static class BoardAnalyzer
    {
        [DebuggerDisplay("{Score}-{Move}")]
        public struct ScoredMove
        {
            public ScoredMove(double score, Move move)
                : this()
            {
                Score = score;
                Move = move;
            }

            public double Score { get; private set; }
            public Move Move { get; private set; }
        }

        public static Move SuggestMove(this Board board)
        {
            var moves = board.MovesFor(board.Turn).Select(board.Score).OrderByDescending(x => x.Score).ToList();

            return moves.Any() ? moves.First().Move : null;
        }

        private static ScoredMove Score(this Board board, Move move)
        {
            var newBoard = board.Do(move);

            var pieceScore = newBoard.PieceScore();

            var mobilityScore = newBoard.MobilityScore();

            var sign = board.Turn == Color.White ? 1 : -1;

            var score = sign * (pieceScore + mobilityScore);

            return new ScoredMove(score, move);
        }
    }


}