using Nancy;
using Nancy.Bootstrapper;
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
}