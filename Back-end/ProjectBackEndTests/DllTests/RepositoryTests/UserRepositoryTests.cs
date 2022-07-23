using System.Linq;
using DLL;
using DLL.Entities;
using DLL.Repositories;
using NUnit.Framework;
using System.Threading.Tasks;
using DLL.Interfaces;

namespace ProjectBackEndTests.DllTests.RepositoryTests
{
    [TestFixture]
    public class UserRepositoryTests
    {
        private DataContext context;

        [SetUp]
        public void SetUp()
        {
            context = new DataContext(UnitTestHelper.GetUnitTestDbOptions());
        }
        [Test]
        public void UserRepository_GetAll_ReturnsAllValues()
        {
            
            var userRepository = new UserRepository(context);

            var users = userRepository.GetAll();

            Assert.AreEqual(6, users.Count(), message: "GetAll method works incorrect");

        }
        [Test]
        public async Task UserRepository_GetAsync_ReturnsSingleValue()
        {
            var userRepository = new UserRepository(context);

            var user = await userRepository.GetAsync(1);
            var firstUser = new User { Email = "TestEmail1@mail.com", NickName = "TestNickName1", Password = "qwe123", Role = "admin" };
            Assert.AreEqual(user.Email, firstUser.Email, message: "GetByIdAsync method works incorrect");
        }

        [Test]
        public async Task UserRepository_AddAsync_AddsValueToDatabase()
        {
            var userRepository = new UserRepository(context);
            var user = new User { Email = "TestEmail7@mail.com", NickName = "TestNickName1", Password = "qwe123", Role = "admin" };
            await userRepository.Create(user);
            await context.SaveChangesAsync();

            var userCheck = context.Users.Last();
            Assert.AreEqual(userCheck.Email, user.Email, message: "AddAsync method works incorrect");
        }

        [Test]
        public async Task UserRepository_DeleteByIdAsync_DeletesEntity()
        {
            var userRepository = new UserRepository(context);
            int usersBefore = userRepository.GetAll().Count(); 
            await userRepository.Delete(4);
            await context.SaveChangesAsync();
            int usersAfter = userRepository.GetAll().Count();
            Assert.AreEqual(usersAfter,--usersBefore, message: "DeleteByIdAsync works incorrect ");

        }

        [Test]
        public async Task UserRepository_Update_UpdatesEntity()
        {
            var userRepository = new UserRepository(context);

            var user = await userRepository.GetAsync(4);
            user.Email = "newUserEmail@mail.com";
            userRepository.Update(user);
            await context.SaveChangesAsync();
            
            Assert.AreEqual("newUserEmail@mail.com", userRepository.GetAsync(4).Result.Email, message: "Update method works incorrect ");

        }
    }
}
