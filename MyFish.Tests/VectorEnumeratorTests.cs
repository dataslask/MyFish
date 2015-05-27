using FluentAssertions;
using MyFish.Brain.Moves;
using NUnit.Framework;

namespace MyFish.Tests
{
    [TestFixture]
    public class VectorEnumeratorTests
    {
        [Test]
        public void Should_move_east_correctly_on_empty_board()
        {
            string.Join(" ", new SliderMoves("d3", TestBoard.With("pd3"), Vector.East)).Should().Be("e3 f3 g3 h3");
        }

        [Test]
        public void Should_move_west_correctly_on_empty_board()
        {
            string.Join(" ", new SliderMoves("d7", TestBoard.With("pd7"), Vector.West)).Should().Be("c7 b7 a7");
        } 
        
        [Test]
        public void Should_move_north_east_correctly_on_empty_board()
        {
            string.Join(" ", new SliderMoves("c5", TestBoard.With("pc5"), Vector.NorthEast)).Should().Be("d6 e7 f8");
        } 
        
        [Test]
        public void Should_move_south_west_correctly_on_empty_board()
        {
            string.Join(" ", new SliderMoves("g5", TestBoard.With("pg5"), Vector.SouthWest)).Should().Be("f4 e3 d2 c1");
        }

        [Test]
        public void Should_stop_when_taking_opponent_pice()
        {
            var board = TestBoard.With("Ra1 pa7");

            string.Join(" ", new SliderMoves("a1", board, Vector.North)).Should().Be("a2 a3 a4 a5 a6 a7");
        }
        
        [Test]
        public void Should_stop_in_front_of_friendly_pices()
        {
            var board = TestBoard.With("ra1 pa7");

            string.Join(" ", new SliderMoves("a1", board, Vector.North)).Should().Be("a2 a3 a4 a5 a6");
        }
    }
}