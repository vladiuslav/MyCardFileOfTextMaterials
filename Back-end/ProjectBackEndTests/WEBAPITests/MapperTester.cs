using NUnit.Framework;
using WEBAPI;
using WEBAPI.Models;
using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;

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
