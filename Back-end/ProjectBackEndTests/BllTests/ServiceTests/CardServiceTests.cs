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
    public class CardServiceTests
    {

        private CardService cardService;
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
            cardService = new CardService(unitOfWork);
        }
        [Test]
        public void CardService_GetAll_ReturnsAllValues()
        {
            var cardsCount = cardService.GetCards().Count();
            Assert.AreEqual(6, cardsCount, message: "GetAll works incorrect ");
        }
        [Test]
        public void CardService_GetCardAsync_ReturnsAllValues()
        {
            var card = cardService.GetCardAsync(1);
            Assert.AreEqual("TestTitle1", card.Result.Title, message: "GetCardAsync works incorrect ");
        }
        [Test]
        public async Task CardService_ChangeCard_ReturnsAllValues()
        {
            var cardDto = new CardDTO
            {
                Id=1,
                Title = "TestTitle100",
                Text = "Some special test text for testing dataBase, some special test text for testing dataBase, ",
                UserId = 1,
                CategoryId = 1,
            };
            await cardService.ChangeCard(cardDto);
            var changedCard = cardService.GetCards().FirstOrDefault(c => c.Id == cardDto.Id);
            Assert.IsNotNull(changedCard, message: "ChangeCard works incorrect ");
        }
    }
}
