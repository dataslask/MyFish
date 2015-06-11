using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using MyFish.Brain;
using MyFish.Brain.Moves;
using MyFish.Tests.Helpers;
using NUnit.Framework;

namespace MyFish.Tests.Scenarios
{
    [TestFixture]
    internal class When_white_is_check_mate_in_move_4 : BoardScenario
    {
        /* 8|r| |b|q|k|b|n|r|
         * 7|p|p|p| | |Q|p|p|
         * 6| | |n|p| | | | |
         * 5| | | | |p| | | |
         * 4| | |B| | | | | |
         * 3| | | | | | | | |
         * 2|P|P|P|P| |P|P|P|
         * 1|R|N|B| |K| |N|R|
         *   A B C D E F G H
         */

        protected override IEnumerable<string> Given()
        {
            yield return "Pe2e4";
            yield return "pe7e5";
            yield return "Qd1h5";
            yield return "nb8c6";
            yield return "Bf1c4";
            yield return "pd7d6";
        }

        protected override Board When(Board currentBoard)
        {
            return currentBoard.Move("Qh5f7");
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

        [Test]
        public void Black_is_check_mate()
        {
            Board.MovesFor(Color.Black).Should().BeEmpty();
        }
    }
}