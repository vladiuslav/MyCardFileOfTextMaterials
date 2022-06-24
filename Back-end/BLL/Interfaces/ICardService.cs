using BLL.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Interfaces
{
    public interface ICardService
    {
        void CreateCard(CardDTO cardDto);
        void ChangeCard(CardDTO cardDto);
        void DeleteCard(int id);
        CardDTO GetCard(int id);
        IEnumerable<CardDTO> GetCards();
        public IEnumerable<CardDTO> GetMostLikedCards();
        public IEnumerable<CardDTO> GetCardsByCategory(int CategoryId);
        void Dispose();
    }
}
