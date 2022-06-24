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
        public void LikeCard(int id)
        {
            _unitOfWork.Cards.Get(id).NumberOfLikes++;
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

        public CardDTO GetCard(int id)
        {
            return _mapper.Map<CardDTO>(_unitOfWork.Cards.Get(id));
        }

        public IEnumerable<CardDTO> GetCards()
        {
            return _mapper.Map<IEnumerable<CardDTO>>(_unitOfWork.Cards.GetAll());
        }

        public IEnumerable<CardDTO> GetMostLikedCards()
        {
            List<CardDTO> cards = _mapper.Map<IEnumerable<CardDTO>>(_unitOfWork.Cards.GetAll()).ToList();
            cards.Sort((x, y) => x.NumberOfLikes.CompareTo(y.NumberOfLikes));
            return cards;
        }
        public IEnumerable<CardDTO> GetCardsByCategory(int CategoryId)
        {
            List<CardDTO> cards = _mapper.Map<IEnumerable<CardDTO>>(_unitOfWork.Cards.GetAll()).ToList();
            cards.FindAll(card => card.CategoryId == CategoryId);
            return cards;
        }

    }
}
