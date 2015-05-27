using MyFish.Web;
using NUnit.Framework;

namespace MyFish.Tests
{
    [SetUpFixture]
    public class AutoMapper
    {
        [SetUp]
        public void Setup()
        {
            Web.AutoMapper.Configure();
        }
    }
}