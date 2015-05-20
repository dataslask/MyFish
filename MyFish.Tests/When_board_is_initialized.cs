using System;
using System.Linq;
using FluentAssertions;
using MyFish.Brain;
using MyFish.Brain.Pieces;
using NUnit.Framework;

namespace MyFish.Tests
{
    [TestFixture]
    internal class When_board_is_initialized : BoardScenario
    {
        protected override Board When(Board board)
        {
            return Fen.Init();
        }

        [Test]
        public void It_should_be_whites_turn()
        {
            Board.Turn.Should().Be(Color.White);
        }

        [Test]
        public void White_can_castle_queenside()
        {
            Board.Can(Castle.Queenside, Color.White).Should().BeTrue();
        }

        [Test]
        public void White_can_castle_kingside()
        {
            Board.Can(Castle.Kingside, Color.White).Should().BeTrue();
        }

        [Test]
        public void There_should_be_32_pieces()
        {
            Board.Pieces.Should().HaveCount(32);
        }

        [Test]
        public void List_of_white_and_black_pieces_should_contain_all_pieces()
        {
            var whiteAndBlack = Board.WhitePieces.Concat(Board.BlackPieces);

            Board.Pieces.Should().Equal(whiteAndBlack);
        }

        [Test]
        public void List_of_white_pieces_should_contain_16_pieces()
        {
            Board.WhitePieces.Should().HaveCount(16);
        }

        [TestCase(typeof(Rook), Color.White, "a1")]
        [TestCase(typeof(Knight), Color.White, "b1")]
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
           
            piece.Should().NotBeNull();
            piece.Position.Should().Be(position);
            piece.Color.Should().Be(color);
        }
    }
}
