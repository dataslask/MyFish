using MyFish.Brain;
using MyFish.Web.Contracts;
using MyFish.Web.Contracts.Commands;
using Nancy;
using Nancy.ModelBinding;
using Board = MyFish.Brain.Board;

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
                
                return Response.AsJsonDto(_board);
            };

            Post["/move"] = parameters =>
            {
                var command = this.Bind<MoveCommand>();

                var piece = AutoMapper.Map<Contracts.Piece, Brain.Piece>(command.Piece);
                var destination = AutoMapper.Map<Contracts.Position, Brain.Position>(command.Destination);

                _board = _board.Move(piece, destination);

                return Response.AsJsonDto(_board);
            };
        }
    }
}