using BLL.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface ICardService
    {
        Task LikeCardAsync(int id);
        Task CreateCard(CardDTO cardDto);
        Task ChangeCard(CardDTO cardDto);
        Task DeleteCardAsync(CardDTO card);
        Task<CardDTO> GetCardAsync(int id);
        Task<IEnumerable<CardDTO>> GetCardsAsync();
        Task<IEnumerable<CardDTO>> GetMostLikedCardsAsync();
        Task<IEnumerable<CardDTO>> GetCardsByCategoryAsync(int CategoryId);
        void DisposeAsync();
    }
}
