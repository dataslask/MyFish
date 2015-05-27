using System.Linq;
using FluentAssertions;
using MyFish.Brain;
using MyFish.Brain.Moves;
using NUnit.Framework;

namespace MyFish.Tests
{
    [TestFixture]
    public class QueenMovesTests
    {
        [Test]
        public void Should_list_all_moves_on_an_empty_board()
        {
            var expected = "c1 c2 c4 c5 c6 c7 c8 a3 b3 d3 e3 f3 g3 h3 a1 b2 d4 e5 f6 g7 h8 a5 b4 d2 e1".Split(' ').Select(x => (Position) x);

            var board = TestBoard.With("qc3");

            new QueenMoves("c3", board).Should().BeEquivalentTo(expected);
        }

        [Test]
        public void Should_stop_when_taking_opponent_pices()
        {
            var expected = "c2 c4 c5 c6 b3 d3 b2 d4 e5 f6 b4 d2".Split(' ').Select(x => (Position)x);

            var board = TestBoard.With("Qc3 pc2 pc6 pb3 pd3 pb2 pf6 pb4 pd2");

            new QueenMoves("c3", board).Should().BeEquivalentTo(expected);
        }

        [Test]
        public void Should_stop_in_front_of_friendly_pices()
        {
            var expected = "c4 c5 b3 d3 b2 d4 e5 f6 a5 b4".Split(' ').Select(x => (Position)x);

            var board = TestBoard.With("qc3 pc2 pc6 pa3 pe3 pa1 pg7 pd2");

            new QueenMoves("c3", board).Should().BeEquivalentTo(expected);
        }
    }
}