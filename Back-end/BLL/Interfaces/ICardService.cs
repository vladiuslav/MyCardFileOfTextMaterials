using BLL.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    /// <summary>
    /// Intarface of service that is used for work with cards and likes. 
    /// </summary>
    public interface ICardService
    {
        /// <summary>
        /// Create Like DTO.
        /// </summary>
        /// <param name="cardId">The Id of card that was liked.</param>
        /// <param name="user">Entity User of card that was liked.</param>
        /// <param name="IsDislike">bool parameter for like or dislike creation.</param>
        Task LikeCard(int cardId, UserDTO user, bool IsDislike = false);
        Task CreateCard(CardDTO cardDto);
        Task ChangeCard(CardDTO cardDto);
        Task DeleteCard(int id);
        Task<CardDTO> GetCardAsync(int id);
        CardDTO GetCardByTitle(string title);
        /// <summary>
        /// Get categories with cards.
        /// </summary>
        /// <returns>Dictionary with id of card and name of category for this card id.</returns>
        Task<Dictionary<int, string>> CategoryNamesAsync();
        /// <summary>
        /// Method give users with cards.
        /// </summary>
        /// <returns>Dictionary with id of card and name of user for this card id.</returns>
        Task<Dictionary<int, string>> UserNamesAsync();
        Task<int> GetCreatorIdByCardIdAsync(int CardId);
        /// <summary>
        /// Method give all cards of user. 
        /// </summary>
        IEnumerable<CardDTO> GetCardsIdByCreatorId(int creatorId);
        /// <summary>
        /// Method give all cards. 
        /// </summary>
        IEnumerable<CardDTO> GetCards();
        /// <summary>
        /// Method give all cards by the category. 
        /// </summary>
        IEnumerable<CardDTO> GetCardsByCategory(string category);
        /// <summary>
        /// Method give cards sorted by date.
        /// </summary>
        IEnumerable<CardDTO> GetCardsSortByDate();
        /// <summary>
        /// Method give cards sorted by number of likes.
        /// </summary>
        IEnumerable<CardDTO> GetCardsSortByPopularity();

        Task Dispose();
    }
}
