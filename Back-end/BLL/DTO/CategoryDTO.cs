using System.Collections.Generic;

namespace BLL.DTO
{
    public class CategoryDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<CardDTO> Cards { get; set; }

        public CategoryDTO()
        {
            this.Cards = new List<CardDTO>();
        }
    }
}
