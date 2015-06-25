using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using MyFish.Brain;
using MyFish.Brain.Exceptions;
using MyFish.Brain.Moves;
using MyFish.Tests.Helpers;
using NUnit.Framework;

namespace MyFish.Tests.Moves
{
    [TestFixture]
    public class PawnMovesTests
    {
        [Test]
        public void Cannot_be_at_rank_1_or_8()
        {
            var board = TestBoard.With("Pb1 Pb8 pc8 pc1");

            ((Action)(() => { new PawnMoves("b1", board); })).ShouldThrow<IllegalPawnPositionException>("white pawn cannot be at rank 1 because it starts at rank 2");
            ((Action)(() => { new PawnMoves("b8", board); })).ShouldThrow<IllegalPawnPositionException>("white pawn cannot be at rank 8 because it should have been promoted");
            ((Action)(() => { new PawnMoves("c8", board); })).ShouldThrow<IllegalPawnPositionException>("black pawn cannot be at rank 8 because it starts at rank 7");
            ((Action)(() => { new PawnMoves("c1", board); })).ShouldThrow<IllegalPawnPositionException>("black pawn cannot be at rank 1 because it should have been promoted");
        }

        [Test]
        public void Can_double_step_from_start_if_not_blocked()
        {
            var board = TestBoard.With("Pb2 pb7 ke8 Ke1");

            new PawnMoves("b2", board).Should().BeEquivalentTo(Expected.Moves("Pb2", "b3 b4"), "white pawn is not blocked");
            new PawnMoves("b7", board).Should().BeEquivalentTo(Expected.Moves("pb7", "b6 b5"), "black pawn is not blocked");
        }

        [Test]
        public void Cannot_double_step_if_first_step_is_blocked()
        {
            var board = TestBoard.With("Pa2 Na3 Pd2 qd3 pc7 nc6 pe7 Pe6");

            new PawnMoves("a2", board).Should().BeEmpty("white pawn at a2 is blocked by white knight at a3");
            new PawnMoves("d2", board).Should().BeEmpty("white pawn at d2 is blocked by black queen at d3");
            new PawnMoves("c7", board).Should().BeEmpty("black pawn at c7 is blocked by black knight at c6");
            new PawnMoves("e7", board).Should().BeEmpty("black pawn at e7 is blocked by white pawn at e6");
        }

        [Test]
        public void Cannot_double_step_if_second_step_is_blocked()
        {
            var board = TestBoard.With("Pc2 Qc4 Pd2 pd4 pc7 qc5 pd7 Pd5 Ke1 ke8");

            new PawnMoves("c2", board).Should().BeEquivalentTo(Expected.Moves("Pc2", "c3"));
            new PawnMoves("d2", board).Should().BeEquivalentTo(Expected.Moves("Pd2", "d3"));
            new PawnMoves("c7", board).Should().BeEquivalentTo(Expected.Moves("pc7", "c6"));
            new PawnMoves("d7", board).Should().BeEquivalentTo(Expected.Moves("pd7", "d6"));
        }

        [Test]
        public void Moves_forward_only_one_step_on_other_ranks()
        {
            var board = TestBoard.With("Pa3 Pc5 pe6 pg5 Ke1 ke8");

            new PawnMoves("a3", board).Should().BeEquivalentTo(Expected.Moves("Pa3", "a4"));
            new PawnMoves("c5", board).Should().BeEquivalentTo(Expected.Moves("Pc5", "c6"));
            new PawnMoves("e6", board).Should().BeEquivalentTo(Expected.Moves("pe6", "e5"));
            new PawnMoves("g5", board).Should().BeEquivalentTo(Expected.Moves("pg5", "g4"));
        }

        [Test]
        public void Cannot_move_forward_if_blocked()
        {
            var board = TestBoard.With("Pa3 Na4 Pc5 pc6 pe6 ne5 pg5 Pg4");

            new PawnMoves("a3", board).Should().BeEmpty("white pawn at a3 is blocked by white knight at a4");
            new PawnMoves("c5", board).Should().BeEmpty("white pawn at c5 is blocked by black pawn at c6");
            new PawnMoves("e6", board).Should().BeEmpty("black pawn at e6 is blocked by black knight at e5");
            new PawnMoves("g5", board).Should().BeEmpty("black pawn at g5 is blocked by white pawn at g4");
        }

        [Test]
        public void Can_take_opponent_left_and_right()
        {
            var board = TestBoard.With("Pb3 pa4 pc4 pe6 Pd5 Pf5 Ke1 ke8");

            new PawnMoves("b3", board).Should().Contain(Expected.Moves("Pb3", "xa4 xc4"));
            new PawnMoves("e6", board).Should().Contain(Expected.Moves("pe6", "xd5 xf5"));
        }
        
        [Test]
        public void Will_not_take_firendly_left_or_right()
        {
            var board = TestBoard.With("Pb3 Pa4 Pc4 pe6 pd5 pf5 Ke1 ke8");

            new PawnMoves("b3", board).Should().NotIntersectWith(Expected.Moves("Pb3", "a4 c4"));
            new PawnMoves("e6", board).Should().NotIntersectWith(Expected.Moves("pe6", "d5 f5"));
        }
        
        [Test]
        public void Will_not_take_firendly_en_passant()
        {
            var board = Fen.Init().Move("Pe2e4");

            var pawnMoves = new PawnMoves("d2", board).Concat(new PawnMoves("f2", board));
            
            var unxepectedMoves = Expected.Moves("Pd2", "e3").Concat(Expected.Moves("Pf2", "e3"));

            pawnMoves.Should().NotIntersectWith(unxepectedMoves);
        }

        [Test]
        public void Can_take_opponent_en_passant_left()
        {
            var board = TestBoard.With("Pb5 pa5 Ke1", "a6");

            new PawnMoves("b5", board).Should().Contain(Expected.Moves("Pb5", "a6"));
        }

        [Test]
        public void Can_take_opponent_en_passant_right()
        {
            var board = TestBoard.With("pe4 Pf4 ke8", "f3", Color.Black);

            new PawnMoves("e4", board).Should().Contain(Expected.Moves("pe4", "f3"));
        }

        [Test]
        public void Must_save_the_queen()
        {
            /* 8| | | | | | | | |
             * 7| | | | | | | | |
             * 6| | | | | | | | |
             * 5| | | | | | | | |
             * 4| | | | | | | | |
             * 3|r| | |K| | | | |
             * 2| |P|P| | |P| | |
             * 1| | | | | | | | |
             *   A B C D E F G H
             */
            var board = TestBoard.With("Kd3 Pb2 Pc2 Pf2 ra3");

            var moves = new PawnMoves("c2", board).Concat(new PawnMoves("f2", board)).Concat(new PawnMoves("b2", board));

            var expected = Expected.Moves("Pc2", "c3").Concat(Expected.Moves("Pb2", "xa3 b3"));

            moves.Should().BeEquivalentTo(expected);
        }

    }
}