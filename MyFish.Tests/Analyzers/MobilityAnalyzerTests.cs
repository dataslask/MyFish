using System.Linq;
using FluentAssertions;
using MyFish.Brain;
using MyFish.Brain.Analyzers;
using MyFish.Brain.Moves;
using MyFish.Brain.Pieces;
using MyFish.Tests.Helpers;
using NUnit.Framework;

namespace MyFish.Tests.Analyzers
{
    [TestFixture]
    public class MobilityAnalyzerTests
    {
        [Test]
        public void Mobility_score_should_be_neutral_on_initial_board()
        {
            var board = Fen.Init();

            board.MobilityScore().Should().Be(0.0);
        }

        [Test]
        public void Mobility_should_be_point_3_when_white_has_three_more_moves()
        {
            var score = TestBoard.With("Ke2 ke8").MobilityScore();

            // White king = 8, black king = 5, score = (8 - 5) * 0.1 = 0.3
            score.Should().BeApproximately(0.3, 0.000001);
        }

        [Test]
        public void Mobility_should_be_minus_point_9_when_black_has_9_more_moves()
        {
            var score = TestBoard.With("Ke1 ke8 rh8").MobilityScore();

            // White king = 5, black king = 5, black rook = 9, score = (5 - (5 + 9) * 0.1 = -0.9
            score.Should().BeApproximately(-0.9, 0.000001);
        }

        [Test]
        public void Mobility_score_should_be_equal_for_1Pe2e3_and_1Pe2e4()
        {
            var board = Fen.Init();

            var scoreE3 = board.Move("Pe2e3").MobilityScore();
            var scoreE4 = board.Move("Pe2e4").MobilityScore();

            scoreE3.Should().Be(scoreE4);
        }
    }
}