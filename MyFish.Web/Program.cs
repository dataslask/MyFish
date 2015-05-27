using System;
using Microsoft.Owin.Hosting;

namespace MyFish.Web
{
    class Program
    {
        static void Main(string[] args)
        {
            const string url = "http://localhost:12345";

            using (WebApp.Start<OwinStartup>(url))
            {
                Console.WriteLine("MyFish.Web running on {0}...", url);
                Console.WriteLine("(hit enter to quit)");
                Console.ReadLine();
            }
        }
    }
}
