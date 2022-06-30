﻿using NUnit.Framework;
using BLL.DTO;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using BLL.MapperConfigurations;
using BLL.Services;

namespace ProjectBackEndTests.BLLTests
{
    [TestFixture]
    public class AccountServiceTesting
    {

        private UserService accountService;
        [SetUp]
        public void Setup()
        {
            accountService = new UserService(@"Server=localhost\SQLEXPRESS;Database=COFT;Trusted_Connection=True;");
        }
        [Test]
        public void AccountService_CreateAccount_ReturnAccount()
        {
            UserDTO userDTO = new UserDTO { Email = "TestEmail10@mail.com", NickName = "TestNickName10", Password = "qwe12345", Role = "user" };
            accountService.CreateUser(userDTO);

            Assert.IsNotNull(accountService.GetUsers().FirstOrDefault(user => user.NickName == "TestNickName10"));
        }
        [Test]
        public void AccountService_GetUsers_ReturnUsers()
        {
            Assert.IsNotNull(accountService.GetUsers());
        }
        [Test]
        public void AccountService_GetUsersFromRange_ReturnUsers()
        {
            int[] idArr = { 1, 10 };
            Assert.IsNotNull(accountService.GetUsersRange(idArr));
        }
        [Test]
        public void AccountService_GetCard_ReturnCard()
        {
            Assert.IsNotNull(accountService.GetUser(1));
        }
    }
}