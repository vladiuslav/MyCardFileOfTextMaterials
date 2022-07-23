using System.Linq;
using DLL;
using DLL.Entities;
using DLL.Repositories;
using NUnit.Framework;
using System.Threading.Tasks;

namespace ProjectBackEndTests.DllTests.RepositoryTests
{
    [TestFixture]
    public class CategoryRepositoryTests
    {
        private DataContext context;

        [SetUp]
        public void SetUp()
        {
            context = new DataContext(UnitTestHelper.GetUnitTestDbOptions());
        }
        [Test]
        public void CategoryRepository_GetAll_ReturnsAllValues()
        {
            var categoryRepository = new CategoryRepository(context);

            var categories = categoryRepository.GetAll();

            Assert.AreEqual(4, categories.Count(), message: "GetAll method works incorrect");

        }
        [Test]
        public async Task CategoryRepository_GetAsync_ReturnsSingleValue()
        {
            var categoryRepository = new CategoryRepository(context);

            var category = await categoryRepository.GetAsync(1);
            var firstCategory = new Category { Name = "TestCategoryName1" };
            Assert.AreEqual(category.Name, firstCategory.Name, message: "GetByIdAsync method works incorrect");
        }

        [Test]
        public async Task CategoryRepository_AddAsync_AddsValueToDatabase()
        {
            var categoryRepository = new CategoryRepository(context);
            var category = new Category { Name = "TestCategoryName5" }; 
            await categoryRepository.Create(category);
            await context.SaveChangesAsync();

            var categoryCheck = context.Categories.Last();
            Assert.AreEqual(categoryCheck.Name, category.Name, message: "AddAsync method works incorrect");
        }

        [Test]
        public async Task CategoryRepository_DeleteByIdAsync_DeletesEntity()
        {
            var categoryRepository = new CategoryRepository(context);
            int categoriesBefore = categoryRepository.GetAll().Count(); 
            await categoryRepository.Delete(3);
            await context.SaveChangesAsync();
            int categoriesAfter = categoryRepository.GetAll().Count();
            Assert.AreEqual(categoriesAfter, --categoriesBefore, message: "DeleteByIdAsync works incorrect ");

        }

        [Test]
        public async Task CategoryRepository_Update_UpdatesEntity()
        {
            var categoryRepository = new CategoryRepository(context);

            var category = await categoryRepository.GetAsync(2);
            category.Name = "TestCategoryName2New";
            categoryRepository.Update(category);
            await context.SaveChangesAsync();
            
            Assert.AreEqual("TestCategoryName2New", categoryRepository.GetAsync(2).Result.Name, message: "Update method works incorrect ");

        }
    }
}
