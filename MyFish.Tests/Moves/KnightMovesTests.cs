using FluentAssertions;
using MyFish.Brain.Moves;
using NUnit.Framework;

namespace MyFish.Tests.Moves
{
    [TestFixture]
    public class KnightMovesTests
    {
        [Test]
        public void Should_list_all_moves_on_an_empty_board()
        {
            var expected = Expected.Moves("c6 e6 c2 e2 b3 b5 f3 f5");

            var board = TestBoard.With("nd4");

            new KnightMoves("d4", board).Should().BeEquivalentTo(expected);
        }

        [Test]
        public void Should_not_move_outside_board()
        {
            var expected = Expected.Moves("a3 c3 d2");

            var board = TestBoard.With("nb1");

            new KnightMoves("b1", board).Should().BeEquivalentTo(expected);
        }

        [Test]
        public void Should_take_opponent_pieces()
        {
            var expected = Expected.Moves("c6 e6 c2 e2 b3 b5 f3 f5");

            var board = TestBoard.With("nd4 Pe6 Rb5");

            new KnightMoves("d4", board).Should().BeEquivalentTo(expected);
        }

        [Test]
        public void Should_not_take_friendly_pieces()
        {
            var expected = Expected.Moves("c6 c2 e2 b3 f3 f5");

            var board = TestBoard.With("nd4 pe6 rb5");

            new KnightMoves("d4", board).Should().BeEquivalentTo(expected);
        }
    }
}