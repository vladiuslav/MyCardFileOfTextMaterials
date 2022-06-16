﻿using NUnit.Framework;
using DLL.Entities;
using DLL.Repositories;
using DLL;
using System.Linq;
using System.Collections.Generic;

namespace ProjectBackEndTests.DLLTests
{
    [TestFixture]
    public class CardRepositoryTests
    {
        private DataContext dataContext;
        private CardRepository cardRepository;
        [SetUp]
        public void Setup()
        {
            dataContext = new DataContext();
            TestHelper.SeedData(dataContext);
            this.cardRepository = new CardRepository(dataContext);
        }

        [Test]
        public void CardRepository_GetCard_ReturnsCard()
        {
            Card CardExcepted = new Card {
                Title = "TestTitle1",
                Text = "Some special test text for testing dataBase, some special test text for testing dataBase, ",
                NumberOfLikes = 0
            };
            Card CardActual = cardRepository.Get(1);
            Assert.AreEqual(CardExcepted.Title, CardActual.Title);
        }

        [Test]
        public void CardRepository_GetALL_ReturnsListOfCard()
        {
            List<Card> Cards = cardRepository.GetAll().ToList();
            Assert.IsNotNull(Cards.FirstOrDefault(Card => Card.Title == "TestTitle1"));
            Assert.IsNotNull(Cards.FirstOrDefault(Card => Card.Title == "TestTitle2"));
        }

        [Test]
        public void CardRepository_Find_ReturnCards()
        {
            var Card = cardRepository.Find(Card => Card.Title == "TestTitle1").FirstOrDefault();
            Assert.IsNotNull(Card);
        }

        [Test]
        public void CardRepository_Update_UpdateCard()
        {
            Card Card = cardRepository.GetAll().First();
            int CardId = Card.Id;
            string nonChangedCardTitle = Card.Title;
            Card.Title = "TestTitle" + System.DateTime.Now.ToString();
            cardRepository.Update(Card);
            dataContext.SaveChanges();
            Card changedCard = cardRepository.Get(CardId);
            Assert.AreNotEqual(nonChangedCardTitle, changedCard.Title);

            changedCard.Title = "TestTitle1";
            cardRepository.Update(changedCard);
            dataContext.SaveChanges();
        }

        [Test]
        public void CardRepository_CreateAndDeleteTesting_CreateCard()
        {
            var Card = new Card
            {
                Title = "TestTitle3",
                Text = "Some special test text for testing dataBase, some special test text for testing dataBase, ",
                NumberOfLikes = 0,
                UserId=1,
                CategoryId=1
            };
            cardRepository.Create(Card);
            dataContext.SaveChanges();
            var CardForDelete = cardRepository.Find(Card => Card.Title == "TestTitle1").FirstOrDefault();
            Assert.IsNotNull(CardForDelete);
            cardRepository.Delete(CardForDelete.Id);
            dataContext.SaveChanges();
            var deletedCard = cardRepository.Get(CardForDelete.Id);
            Assert.IsNull(deletedCard);
        }
    }
}
