using NUnit.Framework;

namespace MyFish.Tests.Web
{
    [SetUpFixture]
    public class AutoMapper
    {
        [SetUp]
        public void Setup()
        {
            MyFish.Web.AutoMapper.Configure();
        }
    }
}