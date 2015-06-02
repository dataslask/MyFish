using System.Linq;
using AutoMapper;
using FluentAssertions;
using MyFish.Brain;
using MyFish.Brain.Pieces;
using MyFish.Tests.Helpers;
using NUnit.Framework;

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

            dto.Color.Should().Be("black");
            dto.Type.Should().Be("rook");
            dto.Position.File.Should().Be('h');
            dto.Position.Rank.Should().Be(8);
        }

        [Test]
        public void Should_map_board_correctly()
        {
            var board = TestBoard.With("Pb2");

            var dto = Mapper.Map<MyFish.Web.Contracts.Board>(board);

            dto.Pieces.Sum(x => x.Length).Should().Be(64);

            for (var i = 0; i < 8; i++)
            {
                for (var j = 0; j < 8; j++)
                {
                    if (!(i == 1 && j == 1))
                    {
                        dto.Pieces[i][j].Should().BeNull("there should be no piece at {0}{1}", (char)('a' + j), i + 1);
                    }
                }
            }
            var piece = dto.Pieces[1][1];

            piece.Should().NotBeNull();
            piece.Type.Should().Be("pawn");
            piece.Color.Should().Be("white");
            piece.Position.File.Should().Be('b');
            piece.Position.Rank.Should().Be(2);
        }
    }
}