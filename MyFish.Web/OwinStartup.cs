using System.Configuration;
using Microsoft.Owin.FileSystems;
using Microsoft.Owin.StaticFiles;
using Nancy.Owin;
using Owin;

namespace MyFish.Web
{
    public class OwinStartup
    {
        public void Configuration(IAppBuilder app)
        {
            var fileSystem = new PhysicalFileSystem(ConfigurationManager.AppSettings["appRoot"]);

            app.Use<ResponseLogger>();
            app.Use<RequestLogger>();

            app.UseDefaultFiles(new DefaultFilesOptions { DefaultFileNames = new[] { "index.html" }, FileSystem = fileSystem });
            app.UseStaticFiles(new StaticFileOptions { FileSystem = fileSystem });
            app.UseNancy();

        }
    }
}