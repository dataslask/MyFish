using FluentAssertions;
using MyFish.Brain;
using MyFish.Brain.Moves;
using NUnit.Framework;

namespace MyFish.Tests.Primitives
{
    [TestFixture]
    public class PositionTests
    {
        [Test]
        public void Invalid_position_member_is_not_valid()
        {
            Position.Invalid.IsValid.Should().BeFalse();
        }

        [Test]
        public void Moving_off_the_board_produces_invalid_position()
        {
            (new Position('h', 8) + Vector.North).IsValid.Should().BeFalse();
            (new Position('h', 8) + Vector.East).IsValid.Should().BeFalse();
            (new Position('a', 1) + Vector.South).IsValid.Should().BeFalse();
            (new Position('a', 1) + Vector.West).IsValid.Should().BeFalse();
        }        
        
        [Test]
        public void Moving_invalid_position_allways_produces_another_invalid_position()
        {
            (new Position('h', 8) + Vector.North + Vector.South).IsValid.Should().BeFalse();
            (new Position('h', 8) + Vector.East + Vector.West).IsValid.Should().BeFalse();
            (new Position('a', 1) + Vector.South + Vector.North).IsValid.Should().BeFalse();
            (new Position('a', 1) + Vector.West + Vector.East).IsValid.Should().BeFalse();
        }

        [Test]
        public void Invalid_position_is_equal_to_some_other_invalid_position()
        {
            var other = new Position('h', 8) + Vector.NorthEast;

            (Position.Invalid == other).Should().BeTrue();
            (Position.Invalid != other).Should().BeFalse();
        }
    }
}