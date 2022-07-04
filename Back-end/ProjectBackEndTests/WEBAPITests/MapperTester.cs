using AutoMapper;
using NUnit.Framework;
using WEBAPI;

namespace ProjectBackEndTests.WEBAPITests
{
    [TestFixture]
    public class MapperTester
    {
        [Test]
        public void MapperTester_Test()
        {
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MapperProfile());
            });
            mapperConfig.AssertConfigurationIsValid();
        }
    }
}
