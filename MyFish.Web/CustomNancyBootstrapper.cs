using System;
using System.Collections.Generic;
using System.Linq;
using Nancy;
using Nancy.Bootstrapper;
using Nancy.ErrorHandling;
using Nancy.Responses;
using Nancy.Responses.Negotiation;
using Nancy.TinyIoc;

namespace MyFish.Web
{
    public class CustomNancyBootstrapper : DefaultNancyBootstrapper
    {
        protected override void ApplicationStartup(TinyIoCContainer container, IPipelines pipelines)
        {
            AutoMapper.Configure();
        }
    }


    public class MyStatusCodeHandler : IStatusCodeHandler
    {
        public bool HandlesStatusCode(HttpStatusCode statusCode, NancyContext context)
        {
            if (statusCode == HttpStatusCode.OK || statusCode == HttpStatusCode.NoContent)
            {
                return false;
            }
            var enumerable = context.Request.Headers.Accept;

            var ranges = enumerable.OrderByDescending(o => o.Item2).Select(o => new MediaRange(o.Item1)).ToList();
            foreach (var item in ranges)
            {
                if (item.Matches("application/json"))
                    return true;
                if (item.Matches("text/json"))
                    return true;
                if (item.Matches("text/html"))
                    return false;
            }

            return false;
        }

        public void Handle(HttpStatusCode statusCode, NancyContext context)
        {          
            context.Response = new JsonResponse("crap", new DefaultJsonSerializer()).WithStatusCode(statusCode);
        }
    }
}