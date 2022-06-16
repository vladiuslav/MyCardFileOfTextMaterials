using DLL;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using DLL.Entities;

namespace ProjectBackEndTests.DLLTests
{
    [TestFixture]
    public class UnitOfWorkTests
    {
        private EFUnitOfWork unitOfWork;
        [SetUp]
        public void Setup()
        {
            var dataContext = new DataContext();
            TestHelper.SeedData(dataContext);
            unitOfWork = new EFUnitOfWork(dataContext);
        }

        [Test]
        public void EFUnitOfWork_Get_repositories()
        {
            Assert.IsNotNull(unitOfWork.Users.GetAll());
            Assert.IsNotNull(unitOfWork.Cards.GetAll());
            Assert.IsNotNull(unitOfWork.Categories.GetAll());
        }
        [Test]
        public void EFUnitOfWork_Save()
        {
            User user = new User { Email = "TestEmail3@mail.com", NickName = "TestNickName3", Password = "qwe123" };
            string userNickName = user.NickName;
            unitOfWork.Users.Create(user);
            unitOfWork.Save();
            Assert.IsNotNull(unitOfWork.Users.GetAll().FirstOrDefault(user => user.NickName==userNickName));

        }
    }
}
