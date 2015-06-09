using System;
using MyFish.Brain.Exceptions;
using Nancy;
using Nancy.Bootstrapper;
using Nancy.Extensions;
using Nancy.TinyIoc;
using Newtonsoft.Json;

namespace MyFish.Web
{
    public class CustomNancyBootstrapper : DefaultNancyBootstrapper
    {
        private static readonly string InternalServerError = HttpStatusCode.InternalServerError.ToString();

        protected override void ConfigureApplicationContainer(TinyIoCContainer container)
        {
            base.ConfigureApplicationContainer(container);

            container.Register<JsonSerializer, CustomJsonSerializer>();
        }

        protected override void ApplicationStartup(TinyIoCContainer container, IPipelines pipelines)
        {
            AutoMapper.Configure();

            pipelines.OnError.AddItemToEndOfPipeline(HandleError);
        }

        private Response HandleError(NancyContext context, Exception exception)
        {
            if (context.AcceptsJson())
            {
                var clientFault = exception is ClientFaultException;

                var statusCode = clientFault ? HttpStatusCode.BadRequest : HttpStatusCode.InternalServerError;
                var message = clientFault ? exception.Message : InternalServerError;
              
                return context.Request.IsLocal()
                    ? new ErrorResponse(message, statusCode, exception)
                    : new ErrorResponse(message, statusCode);
            }
            return null;
        }
    }
}