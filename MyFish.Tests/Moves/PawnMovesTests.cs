using System;
using FluentAssertions;
using MyFish.Brain.Moves;
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

            ((Action)(() => { new PawnMoves("b1", board); })).ShouldThrow<ArgumentException>("white pawn cannot be at rank 1 because it starts at rank 2");
            ((Action)(() => { new PawnMoves("b8", board); })).ShouldThrow<ArgumentException>("white pawn cannot be at rank 8 because it should have been promoted");
            ((Action)(() => { new PawnMoves("c8", board); })).ShouldThrow<ArgumentException>("black pawn cannot be at rank 8 because it starts at rank 7");
            ((Action)(() => { new PawnMoves("c1", board); })).ShouldThrow<ArgumentException>("black pawn cannot be at rank 1 because it should have been promoted");
        }

        [Test]
        public void Can_double_step_from_start_if_not_blocked()
        {
            var board = TestBoard.With("Pb2 pb7");

            new PawnMoves("b2", board).Should().BeEquivalentTo(Expected.Moves("b3 b4"), "white pawn is not blocked");
            new PawnMoves("b7", board).Should().BeEquivalentTo(Expected.Moves("b6 b5"), "black pawn is not blocked");
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
            var board = TestBoard.With("Pc2 Qc4 Pd2 pd4 pc7 qc5 pd7 Pd5");

            new PawnMoves("c2", board).Should().BeEquivalentTo(Expected.Moves("c3"));
            new PawnMoves("d2", board).Should().BeEquivalentTo(Expected.Moves("d3"));
            new PawnMoves("c7", board).Should().BeEquivalentTo(Expected.Moves("c6"));
            new PawnMoves("d7", board).Should().BeEquivalentTo(Expected.Moves("d6"));
        }

        [Test]
        public void Moves_forward_only_one_step_on_other_ranks()
        {
            var board = TestBoard.With("Pa3 Pc5 pe6 pg5");

            new PawnMoves("a3", board).Should().BeEquivalentTo(Expected.Moves("a4"));
            new PawnMoves("c5", board).Should().BeEquivalentTo(Expected.Moves("c6"));
            new PawnMoves("e6", board).Should().BeEquivalentTo(Expected.Moves("e5"));
            new PawnMoves("g5", board).Should().BeEquivalentTo(Expected.Moves("g4"));
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
            var board = TestBoard.With("Pb3 pa4 pc4 pe6 Pd5 Pf5");

            new PawnMoves("b3", board).Should().Contain(Expected.Moves("a4 c4"));
            new PawnMoves("e6", board).Should().Contain(Expected.Moves("d5 f5"));
        }
        
        [Test]
        public void Will_not_take_firendly_left_or_right()
        {
            var board = TestBoard.With("Pb3 Pa4 Pc4 pe6 pd5 pf5");

            new PawnMoves("b3", board).Should().NotContain(Expected.Moves("a4 c4"));
            new PawnMoves("e6", board).Should().NotContain(Expected.Moves("d5 f5"));
        }

        [Test]
        public void Can_take_opponent_en_passant_left()
        {
            var board = TestBoard.With("Pb5 pa5", "a6");

            new PawnMoves("b5", board).Should().Contain(Expected.Moves("a6"));
        }

        [Test]
        public void Can_take_opponent_en_passant_right()
        {
            var board = TestBoard.With("pe4 Pf4", "f3");

            new PawnMoves("e4", board).Should().Contain(Expected.Moves("f3"));
        }

    }
}