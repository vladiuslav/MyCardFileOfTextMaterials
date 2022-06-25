using BLL.Interfaces;
using BLL.DTO;
using BLL.MapperConfigurations;
using System;
using System.Collections.Generic;
using AutoMapper;
using DLL.Entities;
using DLL;
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

            var config = new MapperConfiguration(cfg => {
                cfg.AddProfile<ConfigurationProfile>();
            });
            this._mapper = new Mapper(config);
        }
        public async Task LikeCardAsync(int id)
        {
            (await _unitOfWork.Cards.GetByIdAsync(id)).NumberOfLikes++;
            await _unitOfWork.SaveAsync();
        }
        public async Task CreateCard(CardDTO cardDto)
        {
            _unitOfWork.Cards.Create(_mapper.Map<Card>(cardDto));
            await _unitOfWork.SaveAsync();
        }
        public async Task ChangeCard(CardDTO cardDto)
        {
            _unitOfWork.Cards.Update(_mapper.Map<Card>(cardDto));
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteCardAsync(CardDTO card)
        {
            _unitOfWork.Cards.Delete(_mapper.Map<Card>(card));
            await _unitOfWork.SaveAsync();
        }

        public async void DisposeAsync()
        {
            await _unitOfWork.SaveAsync();
            _unitOfWork.Dispose();
        }

        public async Task<CardDTO> GetCardAsync(int id)
        {
            return _mapper.Map<CardDTO>( await _unitOfWork.Cards.GetByIdAsync(id));
        }

        public async Task<IEnumerable<CardDTO>> GetCardsAsync()
        {
            return _mapper.Map<IEnumerable<CardDTO>>(await _unitOfWork.Cards.GetAllAsync());
        }

        public async Task<IEnumerable<CardDTO>> GetMostLikedCardsAsync()
        {
            List<CardDTO> cards = _mapper.Map<IEnumerable<CardDTO>>(await _unitOfWork.Cards.GetAllAsync()).ToList();
            cards.Sort((x, y) => x.NumberOfLikes.CompareTo(y.NumberOfLikes));
            return cards;
        }
        public async Task<IEnumerable<CardDTO>> GetCardsByCategoryAsync(int CategoryId)
        {
            List<CardDTO> cards = _mapper.Map<IEnumerable<CardDTO>>(await _unitOfWork.Cards.FindAsync(card => card.CategoryId == CategoryId)).ToList();
            return cards;
        }

    }
}
