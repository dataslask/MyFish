using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using MyFish.Brain;

namespace MyFish.Web
{
    public static class AutoMapper
    {
        public static void Configure()
        {
            Mapper.CreateMap<Color, string>().ConvertUsing(x => x.ToString().ToLower());
            Mapper.CreateMap<Position, Contracts.Position>();
            Mapper.CreateMap<Piece, Contracts.Piece>()
                .ForMember(dest => dest.Type, opt => opt.MapFrom(piece => piece.GetType().Name.ToLower()));
            Mapper.CreateMap<Piece[], Contracts.Piece[][]>().ConvertUsing(ToGrid);
            Mapper.CreateMap<Board, Contracts.Board>();
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

        private static readonly Dictionary<Type, Type> DestinationTypeCache = new Dictionary<Type, Type>();

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