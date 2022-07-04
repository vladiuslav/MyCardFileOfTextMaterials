﻿using DLL;
using DLL.Entities;
using NUnit.Framework;
using System.Linq;

namespace ProjectBackEndTests.DLLTests
{
    [TestFixture]
    public class UnitOfWorkTests
    {
        private EFUnitOfWork unitOfWork;
        [SetUp]
        public void Setup()
        {
            unitOfWork = new EFUnitOfWork(@"Server=localhost\SQLEXPRESS;Database=COFT;Trusted_Connection=True;");
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
            User user = new User { Email = "TestEmail3@mail.com", NickName = "TestNickName3", Password = "qwe123", Role = "user" };
            string userNickName = user.NickName;
            unitOfWork.Users.Create(user);
            unitOfWork.Save();
            Assert.IsNotNull(unitOfWork.Users.GetAll().FirstOrDefault(user => user.NickName == userNickName));

        }
    }
}
