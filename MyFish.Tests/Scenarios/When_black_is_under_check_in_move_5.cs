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
    internal class When_black_is_under_check_in_move_5 : BoardScenario
    {
        /* 8|r|n|b| |k|b|n|r|
         * 7|p|p|p| | | |p|p|
         * 6| | | |q|Q| | | |
         * 5| | | | | | | | |
         * 4| | | | | | | | |
         * 3| | | | | | | | |
         * 2|P|P|P|P| |P|P|P|
         * 1|R|N|B| |K|B|N|R|
         *   A B C D E F G H
         */

        protected override IEnumerable<string> Given()
        {
            yield return "Pe2e4";
            yield return "pf7f5";
            yield return "Pe4f5";
            yield return "pe7e6";
            yield return "Pf5e6";
            yield return "pd7e6";
            yield return "Qd1e2";
            yield return "qd8d6";
        }

        protected override Board When(Board currentBoard)
        {
            return currentBoard.Move("Qe2e6");
        }

        [Test]
        public void It_should_be_blacks_turn()
        {
            Board.Turn.Should().Be(Color.Black);
        }

        [Test]
        public void White_should_have_15_pieces()
        {
            Board.WhitePieces.Should().HaveCount(15);
        }

        [Test]
        public void Black_should_have_13_pieces()
        {
            Board.BlackPieces.Should().HaveCount(13);
        }

        [Test]
        public void Black_must_save_the_king()
        {
            var expected = Expected.Moves("ke8", "d8")
                .Concat(Expected.Moves("bc8", "xe6"))
                .Concat(Expected.Moves("bf8", "e7"))
                .Concat(Expected.Moves("ng8", "e7"))
                .Concat(Expected.Moves("qd6", "e7 xe6"));

            Board.MovesFor(Color.Black).Should().BeEquivalentTo(expected);
        }
    }
}