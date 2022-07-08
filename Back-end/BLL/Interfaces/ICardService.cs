using BLL.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface ICardService
    {
        Task LikeCard(int cardId, UserDTO user, bool IsDislike = false);
        Task CreateCard(CardDTO cardDto);
        Task ChangeCard(CardDTO cardDto);
        Task DeleteCard(int id);
        Task<CardDTO> GetCardAsync(int id);
        CardDTO GetCardByTitle(string title);
        Task<Dictionary<int, string>> CategoryNamesAsync();
        Task<Dictionary<int, string>> UserNamesAsync();
        Task<int> GetCreatorIdByCardIdAsync(int CardId);
        IEnumerable<CardDTO> GetCardsIdByCreatorId(int creatorId);
        IEnumerable<CardDTO> GetCards();
        IEnumerable<CardDTO> GetCardsByCategory(string category);
        Task Dispose();
    }
}
