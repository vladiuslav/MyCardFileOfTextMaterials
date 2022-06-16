using NUnit.Framework;
using AutoMapper;
using BLL.MapperConfigurations;
using Moq;
using DLL.Entities;

namespace ProjectBackEndTests.BLLTests
{
    [TestFixture]
    public class BLLTests
    {

        private Mapper mapper;
        private MapperConfiguration config;
        [SetUp]
        public void Setup()
        {
            
            this.config = new MapperConfiguration(cfg => {
                cfg.AddProfile<CardConfigurationProfile>();
                cfg.AddProfile<UserConfigurationProfile>();
                cfg.AddProfile<CategoryConfigurationProfile>();
            });
            this.mapper = new Mapper(config);
        }

        [Test]
        public void Mapping_ConfigurationValidation()
        {
            config.AssertConfigurationIsValid();
            Assert.Pass();
        }
    }
}