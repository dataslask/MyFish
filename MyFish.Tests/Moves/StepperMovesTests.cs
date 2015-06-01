using FluentAssertions;
using MyFish.Brain.Moves;
using MyFish.Brain.Pieces;
using NUnit.Framework;

namespace MyFish.Tests.Moves
{
    [TestFixture]
    public class StepperMovesTests
    {
        private readonly Vector[] _steps = { new Vector(1, 0), new Vector(-1, 0), new Vector(1, 2) };

        [Test]
        public void Should_move_correctly_on_empty_board()
        {
            string.Join(" ", new StepperMoves<Pawn>("d3", TestBoard.With("pd3"), _steps)).Should().Be("e3 c3 e5");
        }

        [Test]
        public void Should_not_move_outside_board()
        {
            string.Join(" ", new StepperMoves<Pawn>("a3", TestBoard.With("pa3"), _steps)).Should().Be("b3 b5");
        }

        [Test]
        public void Should_take_opponent_pieces()
        {
            string.Join(" ", new StepperMoves<Pawn>("d3", TestBoard.With("pd3 Pc3 Re5"), _steps)).Should().Be("e3 c3 e5");
        }

        [Test]
        public void Should_not_take_friendly_pieces()
        {
            string.Join(" ", new StepperMoves<Pawn>("d3", TestBoard.With("pd3 pc3 re5"), _steps)).Should().Be("e3");
        }

    }
}