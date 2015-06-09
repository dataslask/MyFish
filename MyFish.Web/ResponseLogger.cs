using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Owin;

namespace MyFish.Web
{
    public class ResponseLogger : OwinMiddleware
    {
        private readonly OwinMiddleware _next;

        public ResponseLogger(OwinMiddleware next)
            : base(next)
        {
            _next = next;
        }

        public override async Task Invoke(IOwinContext context)
        {
            using (var buffer = new MemoryStream())
            {
                var bodyStream = context.Response.Body;

                context.Response.Body = buffer;

                await _next.Invoke(context);

                Console.WriteLine("[{0}] {1} {2}", Thread.CurrentThread.ManagedThreadId, context.Response.StatusCode, (HttpStatusCode)context.Response.StatusCode);

                foreach (var header in context.Response.Headers)
                {
                    Console.WriteLine("\t{0} {1}", header.Key, string.Join(", ", header.Value));
                }

                buffer.Seek(0, SeekOrigin.Begin);

                using (var reader = new StreamReader(buffer, true))
                {
                    var body = await reader.ReadToEndAsync();
                    Console.WriteLine(body);
                    Console.WriteLine();

                    buffer.Seek(0, SeekOrigin.Begin);
                    await buffer.CopyToAsync(bodyStream);
                }
            }
        }
    }
}