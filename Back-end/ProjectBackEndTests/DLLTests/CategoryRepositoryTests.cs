using DLL;
using DLL.Entities;
using DLL.Repositories;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace ProjectBackEndTests.DLLTests
{
    [TestFixture]
    public class CategoryRepositoryTests
    {
        private DataContext dataContext;
        private CategoryRepository categoryRepository;

        [SetUp]
        public void Setup()
        {
            var contextOptions = new DbContextOptionsBuilder<DataContext>()
            .UseSqlServer(@"Server=localhost\SQLEXPRESS;Database=COFT;Trusted_Connection=True;")
            .Options;
            dataContext = new DataContext(contextOptions);
            this.categoryRepository = new CategoryRepository(dataContext);
        }

        [Test]
        public void CategoryRepository_GetCategory_ReturnsCategory()
        {
            Assert.IsNotNull(categoryRepository.Get(1));
        }

        [Test]
        public void CategoryRepository_GetALL_ReturnsListOfCategory()
        {
            List<Category> categorys = categoryRepository.GetAll().ToList();
            Assert.IsNotNull(categorys.FirstOrDefault(category => category.Name == "TestCategoryName1"));
            Assert.IsNotNull(categorys.FirstOrDefault(category => category.Name == "TestCategoryName2"));
        }

        [Test]
        public void CategoryRepository_Find_ReturnCategorys()
        {
            var category = categoryRepository.Find(category => category.Name == "TestCategoryName1").FirstOrDefault();
            Assert.IsNotNull(category);
        }

        [Test]
        public void CategoryRepository_Update_UpdateCategory()
        {
            Category category = categoryRepository.GetAll().First();
            int categoryId = category.Id;
            string nonChangedCategoryEmail = category.Name;
            category.Name = "TestCategoryName" + System.DateTime.Now.ToString();
            categoryRepository.Update(category);
            dataContext.SaveChanges();
            Category changedCategory = categoryRepository.Get(categoryId);
            Assert.AreNotEqual(nonChangedCategoryEmail, changedCategory.Name);

            changedCategory.Name = "TestCategoryName1";
            categoryRepository.Update(changedCategory);
            dataContext.SaveChanges();
        }

        [Test]
        public void CategoryRepository_CreateAndDeleteTesting_CreateCategory()
        {
            var category = new Category { Name = "TestCategoryName10" };
            categoryRepository.Create(category);
            dataContext.SaveChanges();
            var categoryForDelete = categoryRepository.Find(category => category.Name == "TestCategoryName10").FirstOrDefault();
            Assert.IsNotNull(categoryForDelete);
            categoryRepository.Delete(categoryForDelete.Id);
            dataContext.SaveChanges();
            var deletedCategory = categoryRepository.Get(categoryForDelete.Id);
            Assert.IsNull(deletedCategory);
        }
    }
}
