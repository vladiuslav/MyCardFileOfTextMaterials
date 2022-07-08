using AutoMapper;
using BLL.DTO;
using BLL.Interfaces;
using BLL.MapperConfigurations;
using DLL;
using DLL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class CardService : ICardService
    {

        private EFUnitOfWork _unitOfWork;
        private Mapper _mapper;
        public CardService(string connectionString)
        {
            this._unitOfWork = new EFUnitOfWork(connectionString);

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<ConfigurationProfile>();
            });
            this._mapper = new Mapper(config);
        }
        public async Task CreateCard(CardDTO cardDto)
        {
            cardDto.CreationDate = DateTime.Now;
            await _unitOfWork.Cards.Create(_mapper.Map<Card>(cardDto));
            await _unitOfWork.SaveAsync();
        }
        public async Task LikeCard(int cardId,UserDTO user,bool isDislike=false)
        {

            var like = _unitOfWork.Likes.GetAll().FirstOrDefault(like => like.UserId == user.Id && like.CardId == cardId);
            if (like is null)
            {
                await _unitOfWork.Likes.Create(new Like()
                {
                    IsDislike = isDislike,
                    UserId = user.Id,
                    CardId = cardId
                });
            }
            else
            {
                if (like.IsDislike == isDislike)
                {
                    await _unitOfWork.Likes.Delete(like.Id);
                }
                else
                {
                    like.IsDislike = isDislike;
                    _unitOfWork.Likes.Update(like);
                }
            }
            await _unitOfWork.SaveAsync();

        }
        public async Task ChangeCard(CardDTO cardDto)
        {
            _unitOfWork.Cards.Update(_mapper.Map<Card>(cardDto));
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteCard(int id)
        {
            await _unitOfWork.Cards.Delete(id);
            await _unitOfWork.SaveAsync();
        }

        public async Task Dispose()
        {
            await _unitOfWork.SaveAsync();
            _unitOfWork.Dispose();
        }
        public async Task<Dictionary<int, string>> UserNamesAsync()
        {
            var userNames = new Dictionary<int, string>();
            foreach (var item in _mapper.Map<IEnumerable<CardDTO>>(_unitOfWork.Cards.GetAll()))
            {
                userNames.Add(item.Id, (await _unitOfWork.Users.GetAsync(item.UserId)).NickName);
            }
            return userNames;
        }
        public async Task<Dictionary<int, string>> CategoryNamesAsync()
        {
            var categoryNames = new Dictionary<int, string>();
            foreach (var item in _mapper.Map<IEnumerable<CardDTO>>(_unitOfWork.Cards.GetAll()))
            {
                categoryNames.Add(item.Id,(await _unitOfWork.Categories.GetAsync(item.CategoryId)).Name);
            }
            return categoryNames;
        }
        public async Task<CardDTO> GetCardAsync(int id)
        {
            var card = _mapper.Map<CardDTO>(await _unitOfWork.Cards.GetAsync(id));
            return card; 
        }
        public CardDTO GetCardByTitle(string title)
        {
            return _mapper.Map<CardDTO>(_unitOfWork.Cards.GetAll().FirstOrDefault(card => card.Title == title));
        }
        public async Task<int> GetCreatorIdByCardIdAsync(int CardId)
        {
            return (await GetCardAsync(CardId)).UserId;
        }
        public IEnumerable<CardDTO> GetCardsIdByCreatorId(int creatorId)
        {
            return _mapper.Map<IEnumerable<CardDTO>>(_unitOfWork.Cards.GetAll().FirstOrDefault(card => card.UserId == creatorId));
        }

        public IEnumerable<CardDTO> GetCards()
        {
            return _mapper.Map<IEnumerable<CardDTO>>(_unitOfWork.Cards.GetAll());
        }

        public IEnumerable<CardDTO> GetCardsByCategory(string category)
        {
            List<CardDTO> cards = _mapper.Map<IEnumerable<CardDTO>>(_unitOfWork.Cards.GetAll()).ToList();
            var sortedCards = cards.FindAll(card => card.CategoryId == _unitOfWork.Categories.GetAll().FirstOrDefault(item => item.Name == category).Id);
            sortedCards.Reverse();
            return sortedCards;
        }
    }
}
