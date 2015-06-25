using FluentAssertions;
using MyFish.Brain;
using MyFish.Brain.Analyzers;
using MyFish.Tests.Helpers;
using NUnit.Framework;

namespace MyFish.Tests.Analyzers
{
    [TestFixture]
    public class PieceValueAnalyzerTests
    {
        [Test]
        public void Piece_score_should_be_neutral_on_initial_board()
        {
            var board = Fen.Init();

            board.PieceScore().Should().Be(0.0);
        }

        [Test]
        public void Piece_score_should_be_1_if_white_is_up_by_a_pawn()
        {
            var move = TestBoard.With("Pa2 Pb2 Ke1 pc7 ke8").PieceScore();

            move.Should().Be(1.0);
        }
    
        [Test]
        public void Piece_score_should_be_minus_5_if_black_is_up_by_a_rook()
        {
            var board = TestBoard.With("Pa2 Pb2 Ke1 pc7 pd7 ra8 ke8");

            board.PieceScore().Should().Be(-5.0);
        }
    }
}