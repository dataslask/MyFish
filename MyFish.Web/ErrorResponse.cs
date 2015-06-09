using System;
using Nancy;
using Nancy.Responses;
using Nancy.Serialization.JsonNet;

namespace MyFish.Web
{
    public class ErrorResponse : JsonResponse
    {
        private static readonly ISerializer Serializer = new JsonNetSerializer(new CustomJsonSerializer());

        private ErrorResponse(object model, HttpStatusCode statusCode)
            : base(model, Serializer)
        {
            StatusCode = statusCode;
        }

        public ErrorResponse(string message, HttpStatusCode statusCode)
            : this(new { message }, statusCode)
        {
        }

        public ErrorResponse(string message, HttpStatusCode statusCode, Exception exception)
            : this(new { message, details = exception }, statusCode)
        {
        }
    }
}