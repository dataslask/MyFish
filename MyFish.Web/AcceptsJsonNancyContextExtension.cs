using System.Linq;
using Nancy;
using Nancy.Responses.Negotiation;

namespace MyFish.Web
{
    public static class AcceptsJsonNancyContextExtension
    {
        public static bool AcceptsJson(this NancyContext context)
        {
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
    }
}