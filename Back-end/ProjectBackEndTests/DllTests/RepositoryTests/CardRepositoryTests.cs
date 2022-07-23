using System.Linq;
using DLL;
using DLL.Entities;
using DLL.Repositories;
using NUnit.Framework;
using System.Threading.Tasks;

namespace ProjectBackEndTests.DllTests.RepositoryTests
{
    [TestFixture]
    public class CardRepositoryTests
    {
        private DataContext context;

        [SetUp]
        public void SetUp()
        {
            context = new DataContext(UnitTestHelper.GetUnitTestDbOptions());
        }
        [Test]
        public void CardRepository_GetAll_ReturnsAllValues()
        {
            var cardRepository = new CardRepository(context);

            var cards = cardRepository.GetAll();

            Assert.AreEqual(6, cards.Count(), message: "GetAll method works incorrect");

        }
        [Test]
        public async Task CardRepository_GetAsync_ReturnsSingleValue()
        {
            var cardRepository = new CardRepository(context);

            var card = await cardRepository.GetAsync(1);
            Assert.AreEqual(card.Title, "TestTitle1", message: "GetByIdAsync method works incorrect");
        }

        [Test]
        public async Task CardRepository_AddAsync_AddsValueToDatabase()
        {
            var cardRepository = new CardRepository(context);
            var card = new Card
            {
                Title = "TestTitle100",
                Text = "Some special test text for testing dataBase, some special test text for testing dataBase, ",
                UserId = 1,
                CategoryId = 1,
            };
            await cardRepository.Create(card);
            await context.SaveChangesAsync();

            var cardsCheck = context.Cards.Last();
            Assert.AreEqual(cardsCheck.Title, card.Title, message: "AddAsync method works incorrect");
        }

        [Test]
        public async Task CardRepository_DeleteByIdAsync_DeletesEntity()
        {
            var cardRepository = new CardRepository(context);
            int cardsBefore = cardRepository.GetAll().Count(); 
            await cardRepository.Delete(4);
            await context.SaveChangesAsync();
            int cardsAfter = cardRepository.GetAll().Count();
            Assert.AreEqual(cardsAfter, --cardsBefore, message: "DeleteByIdAsync works incorrect ");

        }

        [Test]
        public async Task CardRepository_Update_UpdatesEntity()
        {
            var cardRepository = new CardRepository(context);

            var card = await cardRepository.GetAsync(4);
            card.Title = "TestTitleNew1";
            cardRepository.Update(card);
            await context.SaveChangesAsync();
            
            Assert.AreEqual("TestTitleNew1", cardRepository.GetAsync(4).Result.Title, message: "Update method works incorrect ");

        }
    }
}
