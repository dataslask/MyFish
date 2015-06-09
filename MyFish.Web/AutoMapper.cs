using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using MyFish.Brain;
using MyFish.Web.Contracts.Commands;

namespace MyFish.Web
{
    public static class AutoMapper
    {
        private static readonly Dictionary<Type, Type> DestinationTypeCache = new Dictionary<Type, Type>();
        private static readonly string ContractColorWhite = Color.White.ToString().ToLower();

        public static void Configure()
        {
            Mapper.CreateMap<Color, string>().ConvertUsing(x => x.ToString().ToLower());
            Mapper.CreateMap<Position, Contracts.Position>();
            Mapper.CreateMap<Piece, Contracts.Piece>()
                .ForMember(dest => dest.Type, opt => opt.MapFrom(piece => piece.GetType().Name.ToLower()));
            Mapper.CreateMap<Board, Contracts.Board>();

            Mapper.CreateMap<Contracts.Position, Position>();
            Mapper.CreateMap<Contracts.Piece, Piece>().ConvertUsing(ToPiece);
        }

        private static Contracts.Piece[] ToArray(Piece[] arg)
        {
            throw new NotImplementedException();
        }

        private static readonly Dictionary<string, char> ContractPiceTypes = new Dictionary<string, char>
        {
            {"rook", 'r'},
            {"knight", 'n'},
            {"bishop", 'b'},
            {"queen", 'q'},
            {"king", 'k'},
            {"pawn", 'p'},
        }; 

        private static Piece ToPiece(Contracts.Piece piece)
        {
            var pieceType = ContractPiceTypes[piece.Type];

            if (piece.Color == ContractColorWhite)
            {
                pieceType = char.ToUpper(pieceType);
            }
            var position = Map<Contracts.Position, Position>(piece.Position);

            return PieceFacory.Create(string.Format("{0}{1}", pieceType, position));
        }

        private static Contracts.Piece[][] ToGrid(Piece[] pieces)
        {
            var dto = new Contracts.Piece[8][];

            for (var rank = 0; rank < 8; rank++)
            {
                dto[rank] = new Contracts.Piece[8];

                for (var file = 0; file < 8; file++)
                {
                    var position = new Position((char) ('a' + file), rank + 1);

                    dto[rank][file] = Mapper.Map<Piece, Contracts.Piece>(pieces.SingleOrDefault(x => x.Position == position));
                }
            }

            return dto;
        }

        public static TDestination Map<TSource, TDestination>(TSource source)
        {
            var destinationType = ToDestinationType(typeof(TSource));

            return (TDestination)Mapper.Map(source, typeof(TSource), destinationType);
        }

        public static object Map<TSource>(TSource source)
        {
            var destinationType = ToDestinationType(typeof(TSource));

            return Mapper.Map(source, typeof(TSource), destinationType);
        }

        public static object Map(object source)
        {
            var sourceType = source.GetType();

            var destinationType = ToDestinationType(sourceType);

            return Mapper.Map(source, sourceType, destinationType);
        }

        private static Type ToDestinationType(Type sourceType)
        {
            if (!DestinationTypeCache.ContainsKey(sourceType))
            {
                lock (DestinationTypeCache)
                {
                    if (!DestinationTypeCache.ContainsKey(sourceType))
                    {
                        DestinationTypeCache.Add(sourceType, Mapper.GetAllTypeMaps().Single(m => m.SourceType == sourceType).DestinationType);
                    }
                }
            }
            return DestinationTypeCache[sourceType];
        }
    }
}