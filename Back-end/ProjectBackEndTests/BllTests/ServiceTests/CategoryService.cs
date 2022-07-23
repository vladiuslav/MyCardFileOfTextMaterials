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
    public class CategoryServiceTests
    {

        private CategoryService categoryService;
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
            categoryService = new CategoryService(unitOfWork);
        }
        [Test]
        public void CategoryService_GetAll_ReturnsAllValues()
        {
            var categoriesCount = categoryService.GetCategories().Count();
            Assert.AreEqual(4, categoriesCount, message: "GetAll works incorrect ");
        }
        [Test]
        public void CategoryService_GetCategoryAsync_ReturnsAllValues()
        {
            var card = categoryService.GetCategoryAsync(1);
            Assert.AreEqual("TestCategoryName1", card.Result.Name, message: "GetCategoryAsync works incorrect ");
        }
        [Test]
        public async Task CategoryService_ChangeCard_ReturnsAllValues()
        {
            var category = new CategoryDTO
            {
                Id=1,
                Name="NewName"
            };
            await categoryService.ChangeCategory(category);
            var changedCard = categoryService.GetCategories().FirstOrDefault(c => c.Id == category.Id);
            Assert.IsNotNull(changedCard, message: "ChangeCategory works incorrect ");
        }
    }
}
