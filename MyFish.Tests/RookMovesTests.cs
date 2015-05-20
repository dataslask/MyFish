﻿using System.Linq;
using FluentAssertions;
using MyFish.Brain;
using MyFish.Brain.Moves;
using NUnit.Framework;

namespace MyFish.Tests
{
    [TestFixture]
    public class RookMovesTests
    {
        [Test]
        public void Should_list_all_moves_on_an_empty_board()
        {
            var expected = "c1 c2 c4 c5 c6 c7 c8 a3 b3 d3 e3 f3 g3 h3".Split(' ').Select(x => (Position) x);

            var board = TestBoard.With("rc3");

            new RookMoves("c3", board).Should().BeEquivalentTo(expected);
        }

        [Test]
        public void Should_stop_when_taking_opponent_pices()
        {
            var expected = "d5 d6 d7 d3 d2 e4 f4 g4 c4 b4".Split(' ').Select(x => (Position) x);

            var board = TestBoard.With("Rd4 pd2 pd7 pb4 pg4");

            new RookMoves("d4", board).Should().BeEquivalentTo(expected);
        }

        [Test]
        public void Should_stop_in_front_of_friendly_pices()
        {
            var expected = "d5 d6 d3 e4 f4 c4".Split(' ').Select(x => (Position) x);

            var board = TestBoard.With("rd4 pd2 pd7 pb4 pg4");

            new RookMoves("d4", board).Should().BeEquivalentTo(expected);
        }
    }
}