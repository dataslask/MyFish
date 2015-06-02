using System.Collections.Generic;
using MyFish.Brain;
using NUnit.Framework;

namespace MyFish.Tests.Scenarios
{
    internal abstract class BoardScenario
    {
        private static class No
        {
            public static IEnumerable<Move> Moves { get { return new Move[0]; } }
        }

        protected Board Board;
        
        [SetUp]
        public void Setup()
        {
            var board = Fen.Init();

            foreach (var move in Given())
            {
                board.Next(move);
            }

            Board = When(board);
        }

        protected virtual IEnumerable<Move> Given()
        {
            return No.Moves;
        }

        protected abstract Board When(Board currentBoard);
    }
}