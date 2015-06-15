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
    internal class When_black_pawn_becomes_enpassant_target : BoardScenario
    {
        /* 8|r|n|b|q|k|b|n|r|
         * 7|p|p|p| |p| |p|p|
         * 6| | | | | | | | |
         * 5| | | |p|P|p| | |
         * 4| | | | | | | | |
         * 3| | | | | | | | |
         * 2|P|P|P|P| |P|P|P|
         * 1|R|N|B|Q|K|B|N|R|
         *   A B C D E F G H
         */

        protected override IEnumerable<string> Given()
        {
            yield return "Pe2e4";
            yield return "pd7d5";
            yield return "Pe4e5";
        }

        protected override Board When(Board currentBoard)
        {
            return currentBoard.Move("pf7f5");
        }

        [Test]
        public void Then_white_can_take_black_pawn_en_passant()
        {
            var expected = Expected.Moves("Pe5", "f6");

            Board.MovesFor(Color.White).Should().Contain(expected);

        }
    }

    [TestFixture]
    internal class When_white_takes_black_pawn_enpassant : BoardScenario
    {
        /* 8|r|n|b|q|k|b|n|r|
         * 7|p|p|p| |p| |p|p|
         * 6| | | | | | | | |
         * 5| | | |p|P|p| | |
         * 4| | | | | | | | |
         * 3| | | | | | | | |
         * 2|P|P|P|P| |P|P|P|
         * 1|R|N|B|Q|K|B|N|R|
         *   A B C D E F G H
         */

        protected override IEnumerable<string> Given()
        {
            yield return "Pe2e4";
            yield return "pd7d5";
            yield return "Pe4e5";
            yield return "pf7f5";
        }

        protected override Board When(Board currentBoard)
        {
            return currentBoard.Move("Pe5f6");
        }

        [Test]
        public void Then_black_should_have_15_pieces()
        {
            Board.BlackPieces.Count().Should().Be(15);
        }

        [Test]
        public void The_black_pawn_is_taken()
        {
            Board.BlackPieces.Should().NotContain(x => x.Position == "f5");
        }
    }
}