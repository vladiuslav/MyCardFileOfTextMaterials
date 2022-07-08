using System.Collections.Generic;

namespace BLL.DTO
{
    /// <summary>
    /// Category DTO (data context object) used working with bll, for mapping card to data logic layer.
    /// </summary>
    public class CategoryDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<CardDTO> Cards { get; set; }

        public CategoryDTO()
        {
            this.Cards = new List<CardDTO>();
        }
    }
}
