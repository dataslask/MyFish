using System;
using FluentAssertions;
using MyFish.Brain;
using MyFish.Brain.Pieces;
using NUnit.Framework;

namespace MyFish.Tests.Scenarios
{
    internal class When_moving_white_knight_on_initial_board : BoardScenario
    {
        protected override Board When(Board currentBoard)
        {
            return currentBoard.Move("Nb1c3");
        }

        [Test]
        public void It_should_be_blacks_turn()
        {
            Board.Turn.Should().Be(Color.Black);
        }

        [TestCase(typeof(Rook), Color.White, "a1")]
        [TestCase(typeof(Knight), Color.White, "c3")]
        [TestCase(typeof(Bishop), Color.White, "c1")]
        [TestCase(typeof(Queen), Color.White, "d1")]
        [TestCase(typeof(King), Color.White, "e1")]
        [TestCase(typeof(Bishop), Color.White, "f1")]
        [TestCase(typeof(Knight), Color.White, "g1")]
        [TestCase(typeof(Rook), Color.White, "h1")]
        [TestCase(typeof(Pawn), Color.White, "a2")]
        [TestCase(typeof(Pawn), Color.White, "b2")]
        [TestCase(typeof(Pawn), Color.White, "c2")]
        [TestCase(typeof(Pawn), Color.White, "d2")]
        [TestCase(typeof(Pawn), Color.White, "e2")]
        [TestCase(typeof(Pawn), Color.White, "f2")]
        [TestCase(typeof(Pawn), Color.White, "g2")]
        [TestCase(typeof(Pawn), Color.White, "h2")]
        [TestCase(typeof(Rook), Color.Black, "a8")]
        [TestCase(typeof(Pawn), Color.Black, "a7")]
        [TestCase(typeof(Pawn), Color.Black, "b7")]
        [TestCase(typeof(Pawn), Color.Black, "c7")]
        [TestCase(typeof(Pawn), Color.Black, "d7")]
        [TestCase(typeof(Pawn), Color.Black, "e7")]
        [TestCase(typeof(Pawn), Color.Black, "f7")]
        [TestCase(typeof(Pawn), Color.Black, "g7")]
        [TestCase(typeof(Pawn), Color.Black, "h7")]
        [TestCase(typeof(Knight), Color.Black, "b8")]
        [TestCase(typeof(Bishop), Color.Black, "c8")]
        [TestCase(typeof(Queen), Color.Black, "d8")]
        [TestCase(typeof(King), Color.Black, "e8")]
        [TestCase(typeof(Bishop), Color.Black, "f8")]
        [TestCase(typeof(Knight), Color.Black, "g8")]
        [TestCase(typeof(Rook), Color.Black, "h8")]
        public void The_pieces_should_be_correctly_placed(Type type, Color color, string position)
        {
            var piece = Board[position];

            piece.Should().NotBeNull("there should be a {0} {1} at {2}", color, type.Name, position);
            piece.Position.Should().Be((Position)position);
            piece.Color.Should().Be(color);
        }

    }
}