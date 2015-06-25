using System.Linq;
using FluentAssertions;
using MyFish.Brain;
using MyFish.Brain.Analyzers;
using MyFish.Brain.Moves;
using MyFish.Tests.Helpers;
using NUnit.Framework;

namespace MyFish.Tests.Analyzers
{
    [TestFixture]
    public class BoardAnalyzerTests
    {
        [Test]
        public void Should_suggest_something()
        {
            var board = Fen.Init();

            board.SuggestMove().Should().NotBeNull();
        }

        [Test]
        public void Prefer_to_take_a_pice_for_white()
        {
            var board = TestBoard.With("Pa2 Pb2 Rc1 Ke1 rc7 ke8");

            board.SuggestMove().Should().Be(Expected.Move("Rc1", "xc7"));
        }

        [Test]
        public void Prefer_to_take_a_pice_for_black()
        {
            var board = TestBoard.With("Pa2 Pb2 Rc1 Ke1 rc7 ke8", null, Color.Black);

            board.SuggestMove().Should().Be(Expected.Move("rc7", "xc1"));
        }

        [Test]
        public void Prefer_to_increase_mobility_for_white()
        {
            /* 8| | | | |k| | | |
             * 7| | | | | | | | |
             * 6| | | | | | | | |
             * 5| | | | | | | | |
             * 4| | | | |x| | | |
             * 3| | | | |x| | |
             * 2|P|P|P|P|P|P|P|P|
             * 1|R|N|B|Q|K|B|N|R|
             *   A B C D E F G H
             */
            var board = Fen.Init();

            var expectedMoves = Expected.Moves("Pe2", "e3 e4");

            var suggestedMove = board.SuggestMove();

            expectedMoves.Should().Contain(suggestedMove);
        }
    }
}