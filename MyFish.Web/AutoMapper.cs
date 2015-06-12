using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using MyFish.Brain;
using MyFish.Brain.Moves;

namespace MyFish.Web
{
    public static class AutoMapper
    {
        public static void Configure()
        {
            Mapper.CreateMap<Color, string>().ConvertUsing(x => x.ToString().ToLower());
            Mapper.CreateMap<Piece, Contracts.Piece>()
                .ForMember(dest => dest.Type, opt => opt.MapFrom(piece => piece.ColoredType));
            Mapper.CreateMap<Board, Contracts.Board>()
                .ForMember(dest => dest.Moves, opt => opt.ResolveUsing(Resolver));
            Mapper.CreateMap<Move, Contracts.Move>();

            Mapper.CreateMap<Contracts.Piece, Piece>().ConvertUsing(piece => PieceFacory.Create(piece.Type[0], piece.Position));
            Mapper.CreateMap<Contracts.Move, Move>();
        }

        private static Dictionary<string, string[]> Resolver(Board board)
        {
            var groups = board.MovesFor(board.Turn)
                .GroupBy(x => x.Piece, x => x.Destination.ToString());

            var dictionary = groups
                .ToDictionary(x => x.Key.Position.ToString(), x => x.ToArray());

            return dictionary;
        }
    }
}