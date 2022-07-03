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
        CardDTO GetCardByTitle(string title);
        Dictionary<int, string> CategoryNames();
        Dictionary<int, string> UserNames();
        int GetCreatorIdByCardId(int CardId);
        IEnumerable<CardDTO> GetCardsIdByCreatorId(int creatorId);
        IEnumerable<CardDTO> GetCards();
        public IEnumerable<CardDTO> GetCardsByCategory(string category);
        void Dispose();
    }
}
