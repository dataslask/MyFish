using System.Linq;
using System.Xml;
using AutoMapper;
using FluentAssertions;
using MyFish.Brain;
using MyFish.Brain.Pieces;
using MyFish.Tests.Helpers;
using MyFish.Web.Contracts;
using NUnit.Framework;
using Move = MyFish.Brain.Move;

namespace MyFish.Tests.Web
{
    [TestFixture]
    public class AutoMapperTests
    {
        [Test]
        public void Should_map_white_correctly()
        {
            Mapper.Map<string>(Color.White).Should().Be("white");
        }

        [Test]
        public void Should_map_black_correctly()
        {
            Mapper.Map<string>(Color.Black).Should().Be("black");
        }

        [Test]
        public void Should_map_piece_correctly()
        {
            var piece = new Rook("h8", Color.Black);

            var dto = Mapper.Map<MyFish.Web.Contracts.Piece>(piece);

            dto.Type.Should().Be("r");
            dto.Position.Should().Be("h8");
        }

        [Test]
        public void Should_map_board_correctly()
        {
            var board = TestBoard.With("Pb2 pa7 Ke1");

            var dto = Mapper.Map<MyFish.Web.Contracts.Board>(board);

            dto.Pieces.Length.Should().Be(3);

            var piece = dto.Pieces[0];

            piece.Should().NotBeNull();
            piece.Type.Should().Be("P");
            piece.Position.Should().Be("b2");

            dto.Moves.Count.Should().Be(2);

            var moves = dto.Moves["b2"];

            moves.Should().BeEquivalentTo(new[] { "b3", "b4" });
        }

        [Test]
        public void Should_map_move_correctly()
        {
            var move = new Move(new King("e1", Color.White), "f2", false);

            var dto = Mapper.Map<MyFish.Web.Contracts.Move>(move);

            var piece = dto.Piece;

            piece.Should().NotBeNull();
            piece.Type.Should().Be("K");
            piece.Position.Should().Be("e1");

            dto.Destination.Should().Be("f2");
        }
    }
}