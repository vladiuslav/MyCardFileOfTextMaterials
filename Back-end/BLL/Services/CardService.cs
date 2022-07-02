using BLL.Interfaces;
using BLL.DTO;
using BLL.MapperConfigurations;
using System;
using System.Collections.Generic;
using AutoMapper;
using DLL.Entities;
using DLL;
using System.Linq;

namespace BLL.Services
{
    public class CardService : ICardService
    {

        private EFUnitOfWork _unitOfWork;
        private Mapper _mapper;
        public CardService(string connectionString)
        {
            this._unitOfWork = new EFUnitOfWork(connectionString);

            var config = new MapperConfiguration(cfg => {
                cfg.AddProfile<ConfigurationProfile>();
            });
            this._mapper = new Mapper(config);
        }
        public void LikeCard(int id,int userId)
        {
            var likes = _mapper.Map<IEnumerable<LikeDTO>>(_unitOfWork.Likes.GetAll());
            if (likes.Any(like=>like.CardId==id&&like.UserId== userId))
            {
                _unitOfWork.Likes.Delete(likes.First(like => like.CardId == id && like.UserId == userId).Id);
            }
            else
            {
                _unitOfWork.Likes.Create(new Like(){UserId=userId,CardId=id });
            }
            _unitOfWork.Save();
        }
        public void CreateCard(CardDTO cardDto)
        {
            _unitOfWork.Cards.Create(_mapper.Map<Card>(cardDto));
            _unitOfWork.Save();
        }
        public void ChangeCard(CardDTO cardDto)
        {
            _unitOfWork.Cards.Update(_mapper.Map<Card>(cardDto));
            _unitOfWork.Save();
        }

        public void DeleteCard(int id)
        {
            _unitOfWork.Cards.Delete(id);
            _unitOfWork.Save();
        }

        public void Dispose()
        {
            _unitOfWork.Save();
            _unitOfWork.Dispose();
        }
        public Dictionary<int,string> UserNames()
        {
            var userNames = new Dictionary<int, string>();
            foreach (var item in _mapper.Map<IEnumerable<CardDTO>>(_unitOfWork.Cards.GetAll()))
            {
                userNames.Add(item.Id, _unitOfWork.Users.Get(item.UserId).NickName);
            }
            return userNames;
        }
        public Dictionary<int, string> CategoryNames()
        {
            var categoryNames = new Dictionary<int, string>();
            foreach (var item in _mapper.Map<IEnumerable<CardDTO>>(_unitOfWork.Cards.GetAll()))
            {
                categoryNames.Add(item.Id, _unitOfWork.Categories.Get(item.CategoryId).Name);
            }
            return categoryNames;
        }
        public CardDTO GetCard(int id)
        {
            return _mapper.Map<CardDTO>(_unitOfWork.Cards.Get(id));
        }
        public int GetCardLikes(int Cardid)
        {
            var card = _unitOfWork.Cards.Get(Cardid).Likes.Count;
            return card;
        }
        public CardDTO GetCardByTitle(string title)
        {
            return _mapper.Map<CardDTO>(_unitOfWork.Cards.GetAll().FirstOrDefault(card => card.Title == title));
        }
        public int GetCreatorIdByCardId(int CardId)
        {
            return GetCard(CardId).UserId;
        }
        public IEnumerable<CardDTO> GetCardsIdByCreatorId(int creatorId)
        {
            return _mapper.Map<IEnumerable<CardDTO>>(_unitOfWork.Cards.GetAll().FirstOrDefault(card=>card.UserId==creatorId));
        }

        public IEnumerable<CardDTO> GetCards()
        {
            return _mapper.Map<IEnumerable<CardDTO>>(_unitOfWork.Cards.GetAll());
        }

        public IEnumerable<CardDTO> GetMostLikedCards()
        {
            List<CardDTO> cards = _mapper.Map<IEnumerable<CardDTO>>(_unitOfWork.Cards.GetAll()).ToList();
            cards.Sort((x, y) => x.Likes.Count.CompareTo(y.Likes.Count));
            cards.Reverse();
            return cards;
        }
        public IEnumerable<CardDTO> GetCardsByCategory(string category)
        {
            List<CardDTO> cards = _mapper.Map<IEnumerable<CardDTO>>(_unitOfWork.Cards.GetAll()).ToList();
            var sortedCards = cards.FindAll(card=>card.CategoryId==_unitOfWork.Categories.Find(item=>item.Name==category).First().Id);
            sortedCards.Reverse();
            return sortedCards;
        }
    }
}
