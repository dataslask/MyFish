using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using MyFish.Brain;
using MyFish.Brain.Moves;
using MyFish.Brain.Pieces;
using MyFish.Tests.Helpers;
using NUnit.Framework;

namespace MyFish.Tests.Primitives
{
    [TestFixture]
    public class SliderMovesTests
    {
        [Test]
        public void Should_move_east_correctly_on_empty_board()
        {
            new SliderMoves<Pawn>("d3", TestBoard.With("pd3 ke8"), true, Vector.East).Join().Should().Be("e3 f3 g3 h3");
        }

        [Test]
        public void Should_move_west_correctly_on_empty_board()
        {
            new SliderMoves<Pawn>("d7", TestBoard.With("pd7 ke8"), true, Vector.West).Join().Should().Be("c7 b7 a7");
        } 
        
        [Test]
        public void Should_move_north_east_correctly_on_empty_board()
        {
            new SliderMoves<Pawn>("c5", TestBoard.With("pc5 ke8"), true, Vector.NorthEast).Join().Should().Be("d6 e7 f8");
        } 
        
        [Test]
        public void Should_move_south_west_correctly_on_empty_board()
        {
            new SliderMoves<Pawn>("g5", TestBoard.With("pg5 ke8"), true, Vector.SouthWest).Join().Should().Be("f4 e3 d2 c1");
        }

        [Test]
        public void Should_move_correctly_when_combining_vectors()
        {
            var moves = new SliderMoves<Pawn>("d3", TestBoard.With("pd3 ke8"), true, Vector.East, Vector.West, Vector.North, Vector.South);

            moves.Join().Should().Be("e3 f3 g3 h3 c3 b3 a3 d4 d5 d6 d7 d8 d2 d1");
        }

        [Test]
        public void Should_stop_when_taking_opponent_pice()
        {
            var board = TestBoard.With("Ra1 pa7 Ke1");

            new SliderMoves<Rook>("a1", board, true, Vector.North).Join().Should().Be("a2 a3 a4 a5 a6 a7");
        }
        
        [Test]
        public void Only_the_last_move_should_be_an_attack_when_taking_opponent_pice()
        {
            var board = TestBoard.With("Ra1 pa7 Ke1");

            var attacks = new SliderMoves<Rook>("a1", board, true, Vector.North).Attacks();

            attacks.Single().Destination.Should().Be((Position) "a7");
        }
        
        [Test]
        public void Should_stop_in_front_of_friendly_pices()
        {
            var board = TestBoard.With("ra1 pa7 ke8");

            new SliderMoves<Rook>("a1", board, true, Vector.North).Join().Should().Be("a2 a3 a4 a5 a6");
        }

        
    }
}