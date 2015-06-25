using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using MyFish.Brain;
using MyFish.Brain.Analyzers;
using MyFish.Brain.Moves;
using Nancy;
using Nancy.ModelBinding;
using Board = MyFish.Brain.Board;
using Move = MyFish.Brain.Move;

namespace MyFish.Web
{
    public class ApiModule : NancyModule
    {
        private static Board _board;

        public ApiModule()
            : base("/api")
        {
            Get["/init"] = _ =>
            {
                _board = Fen.Init();

                var dto = Mapper.Map<Contracts.Board>(_board);

                return Response.AsJson(dto);
            };

            Get["/suggestMove"] = _ =>
            {
                var suggestedMove = _board.SuggestMove();

                var dto = Mapper.Map<Contracts.Move>(suggestedMove);

                return Response.AsJson(dto);
            };

            Post["/move"] = parameters =>
            {
                var move = this.Bind<Contracts.Move>();

                var piece = Mapper.Map<Piece>(move.Piece);
                var destination = Mapper.Map<Position>(move.Destination);

                _board = _board.Move(piece, destination);

                var dto = Mapper.Map<Contracts.Board>(_board);

                return Response.AsJson(dto);
            };
        }
    }
}