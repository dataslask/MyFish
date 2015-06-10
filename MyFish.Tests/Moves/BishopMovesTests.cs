using System.Linq;
using FluentAssertions;
using MyFish.Brain;
using MyFish.Brain.Moves;
using MyFish.Brain.Pieces;
using MyFish.Tests.Helpers;
using NUnit.Framework;

namespace MyFish.Tests.Moves
{
    [TestFixture]
    public class BishopMovesTests
    {
        [Test]
        public void Should_list_all_moves_on_an_empty_board()
        {
            var expected = Expected.Moves("bc3", "d4 e5 f6 g7 h8 b2 a1 d2 e1 b4 a5");

            var board = TestBoard.With("bc3 ke8");

            new BishopMoves("c3", board).Should().BeEquivalentTo(expected);
        }

        [Test]
        public void Should_stop_when_taking_opponent_pices()
        {
            var expected = Expected.Moves("Bd4", "e5 xf6 c3 xb2 c5 xb6 e3 xf2");

            var board = TestBoard.With("Bd4 pf6 pb2 pb6 pf2 Kh1");

            new BishopMoves("d4", board).Should().BeEquivalentTo(expected);
        }

        [Test]
        public void Should_stop_in_front_of_friendly_pices()
        {
            var expected = Expected.Moves("bd4", "e5 f6 c3 b2 c5 b6 e3 f2");

            var board = TestBoard.With("bd4 pg7 pa1 pa7 pg1 kh8");

            new BishopMoves("d4", board).Should().BeEquivalentTo(expected);
        }

        [Test]
        public void Must_save_the_queen()
        {
            /* 8| | | | |k| | | |
             * 7| | | | | | | | |
             * 6| | | | | | | | |
             * 5| |r| | |K| | | |
             * 4| | | | | | | | |
             * 3| | | |B|B| | | |
             * 2| | | | | | | | |
             * 1| | | | | | | | |
             *   A B C D E F G H
             */
            var board = TestBoard.With("Ke5 rb5 Bd3 Be3 ke8");

            var moves = new BishopMoves("d3", board).Concat(new BishopMoves("e3", board));

            var expected = Expected.Moves("Bd3", "xb5").Concat(Expected.Moves("Be3", "c5"));

            moves.Should().BeEquivalentTo(expected);
        }   
    }
}