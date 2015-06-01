using System.Linq;
using FluentAssertions;
using MyFish.Brain;
using MyFish.Brain.Moves;
using NUnit.Framework;

namespace MyFish.Tests.Moves
{
    [TestFixture]
    public class BishopMovesTests
    {
        [Test]
        public void Should_list_all_moves_on_an_empty_board()
        {
            var expected = Expected.Moves("d4 e5 f6 g7 h8 b2 a1 d2 e1 b4 a5");

            var board = TestBoard.With("bc3");

            new BishopMoves("c3", board).Should().BeEquivalentTo(expected);
        }

        [Test]
        public void Should_stop_when_taking_opponent_pices()
        {
            var expected = Expected.Moves("e5 f6 c3 b2 c5 b6 e3 f2");

            var board = TestBoard.With("Bd4 pf6 pb2 pb6 pf2");

            new BishopMoves("d4", board).Should().BeEquivalentTo(expected);
        }

        [Test]
        public void Should_stop_in_front_of_friendly_pices()
        {
            var expected = Expected.Moves("e5 f6 c3 b2 c5 b6 e3 f2");

            var board = TestBoard.With("bd4 pg7 pa1 pa7 pg1");

            new BishopMoves("d4", board).Should().BeEquivalentTo(expected);
        }
    }
}