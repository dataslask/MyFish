using System.Linq;
using FluentAssertions;
using MyFish.Brain;
using MyFish.Brain.Moves;
using MyFish.Tests.Helpers;
using NUnit.Framework;

namespace MyFish.Tests.Moves
{
    [TestFixture]
    public class KingMovesTests
    {
        [Test]
        public void Should_list_all_moves_on_an_empty_board()
        {
            var expected = Expected.Moves("Kd5", "d6 e6 e5 e4 d4 c4 c5 c6");

            var board = TestBoard.With("Kd5");

            new KingMoves("d5", board).Should().BeEquivalentTo(expected);
        }

        [Test]
        public void Should_not_move_outside_board()
        {
            var expected = Expected.Moves("kh8", "h7 g7 g8");

            var board = TestBoard.With("kh8");

            new KingMoves("h8", board).Should().BeEquivalentTo(expected);
        }

        [Test]
        public void Should_take_opponent_pieces()
        {
            var expected = Expected.Moves("Kd5", "d6 e6 e5 e4 d4 c4 c5 c6");

            var board = TestBoard.With("Kd5 pe6 pd4 pc6");

            new KingMoves("d5", board).Should().BeEquivalentTo(expected);
        }

        [Test]
        public void Should_not_take_friendly_pieces()
        {
            var expected = Expected.Moves("Kd5", "d6 e5 e4 c4 c5");

            var board = TestBoard.With("Kd5 Pe6 Pd4 Pc6");

            new KingMoves("d5", board).Should().BeEquivalentTo(expected);
        }

        [Test]
        public void Cannot_put_it_self_in_chess()
        {
            /* 8| | | | | | | | |
             * 7| | | | | |n| | |
             * 6| |p| | | | | | |
             * 5| | | |K| | | | |
             * 4|r| | | | | | | |
             * 3| | | | | | | | |
             * 2| | | | |r| | | |
             * 1| | | | | | |b| |
             *   A B C D E F G H
             */
            var board = TestBoard.With("Kd5 pb6 ra4 re2 nf7 bg1");

            var moves = new KingMoves("d5", board).ToArray();

            moves.Should().Equal(Expected.Moves("Kd5", "c6"));
        }
    }
}