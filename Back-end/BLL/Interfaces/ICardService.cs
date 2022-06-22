using BLL.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Interfaces
{
    public interface ICardService
    {
        void CreateCard(CardDTO cardDto);
        CardDTO GetCard(int id);
        IEnumerable<CardDTO> GetCards();
        void Dispose();
    }
}
