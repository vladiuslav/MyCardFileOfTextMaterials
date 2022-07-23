using System.Linq;
using DLL;
using NUnit.Framework;
using System.Threading.Tasks;
using Moq;
using DLL.Interfaces;
using DLL.Repositories;
using BLL.Services;
using BLL.DTO;

namespace ProjectBackEndTests.BllTests.ServiceTests
{
    [TestFixture]
    public class UserServiceTests
    {

        private UserService userService;
        [SetUp]
        public void SetUp()
        {
            DataContext dataContext = new DataContext(UnitTestHelper.GetUnitTestDbOptions());
            CardRepository cardRepository = new CardRepository(dataContext);
            UserRepository userRepository = new UserRepository(dataContext);
            LikeRepository likeRepository = new LikeRepository(dataContext);
            CategoryRepository categoryRepository = new CategoryRepository(dataContext);
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(uow => uow.Likes).Returns(likeRepository);
            mockUnitOfWork.Setup(uow => uow.Cards).Returns(cardRepository);
            mockUnitOfWork.Setup(uow => uow.Users).Returns(userRepository);
            mockUnitOfWork.Setup(uow => uow.Categories).Returns(categoryRepository);
            IUnitOfWork unitOfWork = mockUnitOfWork.Object;
            userService = new UserService(unitOfWork);
        }
        [Test]
        public void UserService_GetAll_ReturnsAllValues()
        {
            var usersCount = userService.GetUsers().Count();
            Assert.AreEqual(6, usersCount, message: "GetAll works incorrect ");
        }
        [Test]
        public void UserService_GetCardAsync_ReturnsAllValues()
        {
            var user = userService.GetUser(1);
            Assert.AreEqual("TestNickName1", user.Result.NickName, message: "GetCardAsync works incorrect ");
        }
        [Test]
        public async Task UserService_ChangeUser_ReturnsAllValues()
        {
            var userDto = new UserDTO
            {
                Id=1,
                Email = "TestEmail11@mail.com",
                NickName = "TestNickName11",
            };
            await userService.ChangeUser(userDto);
            var changedCard = userService.GetUsers().FirstOrDefault(c => c.Id == userDto.Id);
            Assert.IsNotNull(changedCard, message: "ChangeCard works incorrect ");
        }
    }
}
