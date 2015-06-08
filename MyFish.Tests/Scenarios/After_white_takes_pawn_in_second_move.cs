using System.Collections.Generic;
using FluentAssertions;
using MyFish.Brain;
using NUnit.Framework;

namespace MyFish.Tests.Scenarios
{
    [TestFixture]
    internal class After_white_takes_pawn_in_second_move : BoardScenario
    {
        /* 8|r|n|b|q|k|b|n|r|
         * 7|p|p|p| |p|p|p|p|
         * 6| | | | | | | | |
         * 5| | | |p| | | | |
         * 4| | | | | | | | |
         * 3| | |N| | | | | |
         * 2|P|P|P|P|P|P|P|P|
         * 1|R| |B|Q|K|B|N|R|
         *   A B C D E F G H
         */

        protected override IEnumerable<string> Given()
        {
            yield return "Nb1c3";
            yield return "pd7d5";
        }

        protected override Board When(Board currentBoard)
        {
            return currentBoard.Move("Nc3d5");
        }

        [Test]
        public void It_should_be_blacks_turn()
        {
            Board.Turn.Should().Be(Color.Black);
        }

        [Test]
        public void White_should_have_16_pieces()
        {
            Board.WhitePieces.Should().HaveCount(16);
        }

        [Test]
        public void Black_should_have_15_pieces()
        {
            Board.BlackPieces.Should().HaveCount(15);
        }
    }
}