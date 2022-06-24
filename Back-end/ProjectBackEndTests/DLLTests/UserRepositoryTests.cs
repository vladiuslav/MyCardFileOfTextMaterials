using NUnit.Framework;
using DLL.Entities;
using DLL.Repositories;
using DLL;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ProjectBackEndTests.DLLTests
{
    [TestFixture]
    public class UserRepositoryTests
    {
        private DataContext dataContext;
        private UserRepository userRepository;
        [SetUp]
        public void Setup()
        {
            var contextOptions = new DbContextOptionsBuilder<DataContext>()
            .UseSqlServer(@"Server=localhost\SQLEXPRESS;Database=COFT;Trusted_Connection=True;")
            .Options;
            dataContext = new DataContext(contextOptions);
            this.userRepository = new UserRepository(dataContext);
        }

        [Test]
        public void UserRepository_GetUser_ReturnsUser()
        {
            User userExcepted = new User {Email = "TestEmail1@mail.com", NickName = "TestNickName1", Password = "qwe123", Role = "user" };
            User userActual = userRepository.Get(1);
            Assert.AreEqual(userExcepted.Email, userActual.Email);
            Assert.AreEqual(userExcepted.NickName, userActual.NickName);
            Assert.AreEqual(userExcepted.Password, userActual.Password);
        }

        [Test]
        public void UserRepository_GetALL_ReturnsListOfUser()
        {
            List<User> users = userRepository.GetAll().ToList();
            Assert.IsNotNull(users.FirstOrDefault(user=>user.NickName == "TestNickName1"));
            Assert.IsNotNull(users.FirstOrDefault(user=>user.NickName == "TestNickName2"));
        }

        [Test]
        public void UserRepository_Find_ReturnUsers()
        {
            var user = userRepository.Find(user => user.Email == "TestEmail1@mail.com").FirstOrDefault();
            Assert.IsNotNull(user);
        }

        [Test]
        public void UserRepository_Update_UpdateUser()
        {
            User user = userRepository.GetAll().First();
            int userId = user.Id;
            string nonChangedUserEmail = user.Email;
            user.Email = "TestEmail" + System.DateTime.Now.ToString() + "@gmail.com";
            userRepository.Update(user);
            dataContext.SaveChanges();
            User changedUser= userRepository.Get(userId);
            Assert.AreNotEqual(nonChangedUserEmail, changedUser.Email);

            changedUser.Email = "TestEmail1@mail.com";
            userRepository.Update(changedUser);
            dataContext.SaveChanges();
        }

        [Test]
        public void UserRepository_CreateAndDeleteTesting_CreateUser()
        {
            var user = new User { Email = "TestEmail3@mail.com", NickName = "TestNickName3", Password = "qwe123", Role = "user" };
            userRepository.Create(user);
            dataContext.SaveChanges();
            var userForDelete = userRepository.Find(user => user.Email == "TestEmail3@mail.com").FirstOrDefault();
            Assert.IsNotNull(userForDelete);
            userRepository.Delete(userForDelete.Id);
            dataContext.SaveChanges();
            var deletedUser = userRepository.Get(userForDelete.Id);
            Assert.IsNull(deletedUser);
        }
    }
}
