using System.Linq;
using FluentAssertions;
using MyFish.Brain.Moves;
using MyFish.Tests.Helpers;
using NUnit.Framework;

namespace MyFish.Tests.Moves
{
    [TestFixture]
    public class KnightMovesTests
    {
        [Test]
        public void Should_list_all_moves_on_an_empty_board()
        {
            var expected = Expected.Moves("nd4", "c6 e6 c2 e2 b3 b5 f3 f5");

            var board = TestBoard.With("nd4 ke8");

            new KnightMoves("d4", board).Should().BeEquivalentTo(expected);
        }

        [Test]
        public void Should_not_move_outside_board()
        {
            var expected = Expected.Moves("nb1", "a3 c3 d2");

            var board = TestBoard.With("nb1 ke8");

            new KnightMoves("b1", board).Should().BeEquivalentTo(expected);
        }

        [Test]
        public void Should_take_opponent_pieces()
        {
            var expected = Expected.Moves("nd4", "c6 xe6 c2 e2 b3 xb5 f3 f5");

            var board = TestBoard.With("nd4 Pe6 Rb5 ke8");

            new KnightMoves("d4", board).Should().BeEquivalentTo(expected);
        }

        [Test]
        public void Should_not_take_friendly_pieces()
        {
            var expected = Expected.Moves("nd4", "c6 c2 e2 b3 f3 f5");

            var board = TestBoard.With("nd4 pe6 rb5 ke8");

            new KnightMoves("d4", board).Should().BeEquivalentTo(expected);
        }

        [Test]
        public void Must_save_the_queen()
        {
            /* 8| | | | |k| | | |
             * 7| | | | | | | | |
             * 6| | | | | | | | |
             * 5| |r| | |K| | | |
             * 4| | | | | | | | |
             * 3| | |N| | | | | |
             * 2| | | | | | | | |
             * 1| | | | | | | | |
             *   A B C D E F G H
             */
            var board = TestBoard.With("Ke5 rb5 Nc3 ke8");

            new KnightMoves("c3", board).Should().BeEquivalentTo(Expected.Moves("Nc3", "xb5 d5"));
        }        
    }
}