using System.Linq;
using AutoMapper;
using MyFish.Brain;
using Nancy;

namespace MyFish.Web
{
    public class ApiModule : NancyModule
    {
        public ApiModule()
            : base("/api")
        {
            Get["/init"] = _ =>
            {
                var board = Fen.Init();
                
                return Response.AsJsonDto(board);
            };
        }
    }
}