using System.Linq;
using FluentAssertions;
using MyFish.Brain;
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
            string.Join(" ", new VectorEnumerator("d3", Vector.East, TestBoard.Empty(), Color.Black)).Should().Be("e3 f3 g3 h3");
        }

        [Test]
        public void Should_move_west_correctly_on_empty_board()
        {
            string.Join(" ", new VectorEnumerator("d7", Vector.West, TestBoard.Empty(), Color.Black)).Should().Be("c7 b7 a7");
        } 
        
        [Test]
        public void Should_move_north_east_correctly_on_empty_board()
        {
            string.Join(" ", new VectorEnumerator("c5", Vector.NorthEast, TestBoard.Empty(), Color.Black)).Should().Be("d6 e7 f8");
        } 
        
        [Test]
        public void Should_move_south_west_correctly_on_empty_board()
        {
            string.Join(" ", new VectorEnumerator("g5", Vector.SouthWest, TestBoard.Empty(), Color.Black)).Should().Be("f4 e3 d2 c1");
        }

        [Test]
        public void Should_stop_when_taking_opponent_pice()
        {
            var board = TestBoard.With("pa7");

            string.Join(" ", new VectorEnumerator("a1", Vector.North, board, Color.White)).Should().Be("a2 a3 a4 a5 a6 a7");
        }
        
        [Test]
        public void Should_stop_in_front_of_other_friendly_pices()
        {
            var board = TestBoard.With("pa7");

            string.Join(" ", new VectorEnumerator("a1", Vector.North, board, Color.Black)).Should().Be("a2 a3 a4 a5 a6");
        }
    }
}