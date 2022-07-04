using BLL.DTO;
using BLL.Services;
using NUnit.Framework;
using System.Linq;

namespace ProjectBackEndTests.BLLTests
{

    [TestFixture]
    public class CardServiceTesting
    {
        private CardService cardService;
        [SetUp]
        public void Setup()
        {
            cardService = new CardService(@"Server=localhost\SQLEXPRESS;Database=COFT;Trusted_Connection=True;");
        }

        [Test]
        public void CardService_CreateCard_ReturnCard()
        {
            CardDTO cardDTO = new CardDTO
            {
                Title = "TestTitle11",
                Text = "Some special test text for testing dataBase, some special test text for testing dataBase, ",
                UserId = 1,
                CategoryId = 1
            };
            cardService.CreateCard(cardDTO);

            Assert.IsNotNull(cardService.GetCards().FirstOrDefault(card => card.Title == "TestTitle11"));
        }
        [Test]
        public void CardService_GetCards_ReturnCards()
        {
            Assert.IsNotNull(cardService.GetCards());
        }
        [Test]
        public void CardService_GetCard_ReturnCard()
        {
            Assert.IsNotNull(cardService.GetCard(1));
        }
    }
}
