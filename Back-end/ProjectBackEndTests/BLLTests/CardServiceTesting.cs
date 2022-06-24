using NUnit.Framework;
using BLL.DTO;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using BLL.MapperConfigurations;
using BLL.Services;

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
            CardDTO cardDTO = new CardDTO {
                Title = "TestTitle11",
                Text = "Some special test text for testing dataBase, some special test text for testing dataBase, ",
                NumberOfLikes = 0,
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
        public void CardService_LikeCard_ReturnCard()
        {
            int cardLikesBefore = cardService.GetCard(1).NumberOfLikes;
            cardService.LikeCard(1);
            int cardLikesAfter = cardService.GetCard(1).NumberOfLikes;
            Assert.AreEqual(cardLikesBefore+1, cardLikesAfter);
        }
        [Test]
        public void CardService_GetCard_ReturnCard()
        {
            Assert.IsNotNull(cardService.GetCard(1));
        }
    }
}
