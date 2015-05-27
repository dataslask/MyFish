using FluentAssertions;
using MyFish.Brain.Moves;
using NUnit.Framework;

namespace MyFish.Tests
{
    [TestFixture]
    public class MultiVectorEnumeratorTests
    {
        [Test]
        public void Should_move_correctly_on_empty_board()
        {
            var enumerator = new SliderMoves("d3", TestBoard.With("pd3"), Vector.East, Vector.West, Vector.North, Vector.South);

            string.Join(" ", enumerator).Should().Be("e3 f3 g3 h3 c3 b3 a3 d4 d5 d6 d7 d8 d2 d1");
        }
    }
}