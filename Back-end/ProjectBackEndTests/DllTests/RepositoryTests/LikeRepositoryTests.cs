using System.Linq;
using DLL;
using DLL.Entities;
using DLL.Repositories;
using NUnit.Framework;
using System.Threading.Tasks;

namespace ProjectBackEndTests.DllTests.RepositoryTests
{
    [TestFixture]
    public class LikeRepositoryTests
    {
        private DataContext context;

        [SetUp]
        public void SetUp()
        {
            context = new DataContext(UnitTestHelper.GetUnitTestDbOptions());
        }
        [Test]
        public void LikeRepository_GetAll_ReturnsAllValues()
        {
            var likeRepository = new LikeRepository(context);

            var likes = likeRepository.GetAll();

            Assert.AreEqual(4, likes.Count(), message: "GetAll method works incorrect");

        }
        [Test]
        public async Task LikeRepository_GetAsync_ReturnsSingleValue()
        {
            var likeRepository = new LikeRepository(context);

            var like = await likeRepository.GetAsync(1);
            var firstLike = new Like()
            {
                IsDislike = false,
                UserId = 1,
                CardId = 1
            };
            Assert.AreEqual(like.UserId, firstLike.UserId, message: "GetByIdAsync method works incorrect");
            Assert.AreEqual(like.CardId, firstLike.CardId, message: "GetByIdAsync method works incorrect");
        }

        [Test]
        public async Task LikeRepository_AddAsync_AddsValueToDatabase()
        {
            var likeRepository = new LikeRepository(context);
            var like = new Like()
            {
                IsDislike = false,
                UserId = 2,
                CardId = 2
            }; 
            await likeRepository.Create(like);
            await context.SaveChangesAsync();

            var likeCheck = context.Likes.Last();
            Assert.AreEqual(likeCheck.CardId, like.CardId, message: "AddAsync method works incorrect");
            Assert.AreEqual(likeCheck.UserId, like.UserId, message: "AddAsync method works incorrect");
        }

        [Test]
        public async Task LikeRepository_DeleteByIdAsync_DeletesEntity()
        {
            var likeRepository = new LikeRepository(context);
            int likeBefore = likeRepository.GetAll().Count(); 
            await likeRepository.Delete(4);
            await context.SaveChangesAsync();
            int usersAfter = likeRepository.GetAll().Count();
            Assert.AreEqual(usersAfter,--likeBefore, message: "DeleteByIdAsync works incorrect ");

        }

        [Test]
        public async Task LikeRepository_Update_UpdatesEntity()
        {
            var likeRepository = new LikeRepository(context);

            var like = await likeRepository.GetAsync(4);
            like.IsDislike = !like.IsDislike;
            var isDislike = like.IsDislike;
            likeRepository.Update(like);
            await context.SaveChangesAsync();
            
            Assert.AreEqual(isDislike, likeRepository.GetAsync(4).Result.IsDislike, message: "Update method works incorrect ");

        }
    }
}
