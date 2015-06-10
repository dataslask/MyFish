using System;
using FluentAssertions;
using MyFish.Brain;
using MyFish.Brain.Exceptions;
using MyFish.Tests.Helpers;
using NUnit.Framework;

namespace MyFish.Tests
{
    [TestFixture]
    public class BoardTests
    {
        [Test]
        public void Should_be_able_to_move_a_pawn()
        {
            var board = TestBoard.With("Pb3 Ke1");

            var newBoard = board.Move("Pb3b4");

            var expected = TestBoard.With("Pb4 Ke1");

            newBoard.Pieces.Should().BeEquivalentTo(expected.Pieces);
        }

        [Test]
        public void Should_throw_if_move_is_illegal()
        {
            var board = TestBoard.With("Qe5 Ke1");

            Action move = () => board.Move("Qe5f7");

            move.ShouldThrow<IllegalMoveException>();
        }

        [Test]
        public void Should_throw_if_moving_wrong_color()
        {
            var board = TestBoard.With("Qe5", null, Color.Black);

            Action move = () => board.Move("Qe5f6");

            move.ShouldThrow<WrongTurnException>();
        }

        [Test]
        public void Opponent_piece_should_be_removed_when_attacking()
        {
            var board = TestBoard.With("nd4 Pe6 Rb5 kh8", null, Color.Black);

            var newBoard = board.Move("nd4e6");

            var expected = TestBoard.With("ne6 Rb5 kh8");

            newBoard.Pieces.Should().BeEquivalentTo(expected.Pieces);
        }

        [Test]
        public void It_is_blacks_turn_after_white_moves()
        {
            var board = TestBoard.With("Re4 Kh1");

            var newBoard = board.Move("Re4e8");

            newBoard.Turn.Should().Be(Color.Black);
        }

    }
}