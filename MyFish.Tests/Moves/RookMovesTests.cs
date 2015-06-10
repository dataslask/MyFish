using System.Linq;
using FluentAssertions;
using MyFish.Brain;
using MyFish.Brain.Moves;
using MyFish.Tests.Helpers;
using NUnit.Framework;

namespace MyFish.Tests.Moves
{
    [TestFixture]
    public class RookMovesTests
    {
        [Test]
        public void Should_list_all_moves_on_an_empty_board()
        {
            var expected = Expected.Moves("rc3", "c1 c2 c4 c5 c6 c7 c8 a3 b3 d3 e3 f3 g3 h3");

            var board = TestBoard.With("rc3 ke1");

            new RookMoves("c3", board).Should().BeEquivalentTo(expected);
        }

        [Test]
        public void Should_stop_when_taking_opponent_pices()
        {
            var expected = Expected.Moves("Rd4", "d5 d6 xd7 d3 xd2 e4 f4 xg4 c4 xb4");

            var board = TestBoard.With("Rd4 pd2 pd7 pb4 pg4 Kh1");

            new RookMoves("d4", board).Should().BeEquivalentTo(expected);
        }

        [Test]
        public void Should_stop_in_front_of_friendly_pices()
        {
            var expected = Expected.Moves("rd4", "d5 d6 d3 e4 f4 c4");

            var board = TestBoard.With("rd4 pd2 pd7 pb4 pg4 kh8");

            new RookMoves("d4", board).Should().BeEquivalentTo(expected);
        }

        [Test]
        public void Must_save_the_queen()
        {
            /* 8| | | | |k| | | |
             * 7| | |R| | | | | |
             * 6| | | | | | | | |
             * 5| |r| | |K| | | |
             * 4| | | | | | | | |
             * 3| |R| | | | | |
             * 2| | | | | | | | |
             * 1| | | | | | | | |
             *   A B C D E F G H
             */
            var board = TestBoard.With("Ke5 rb5 Rb3 Rc7 ke8");

            var moves = new RookMoves("b3", board).Concat(new RookMoves("c7", board));

            var expected = Expected.Moves("Rb3", "xb5").Concat(Expected.Moves("Rc7", "c5"));

            moves.Should().BeEquivalentTo(expected);
        }   
    }
}