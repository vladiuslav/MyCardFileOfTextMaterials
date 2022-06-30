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
<<<<<<< HEAD
        CardDTO GetCardByTitle(string title);
        Dictionary<int, string> CategoryNames();
        Dictionary<int, string> UserNames();
        int GetCreatorIdByCardId(int CardId);
        IEnumerable<CardDTO> GetCardsIdByCreatorId(int creatorId);
=======
        int GetCardCreatorId(int CardId);
>>>>>>> d03a1e310e61c70dd64bb6110d8342ddf81f75d5
        IEnumerable<CardDTO> GetCards();
        public IEnumerable<CardDTO> GetMostLikedCards();
        public IEnumerable<CardDTO> GetCardsByCategory(int CategoryId);
        void Dispose();
    }
}
