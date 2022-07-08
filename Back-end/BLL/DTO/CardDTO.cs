using System;
using System.Collections.Generic;

namespace BLL.DTO
{
    public class CardDTO
    {
        public int Id;
        public string Title { get; set; }
        public string Text { get; set; }
        public DateTime CreationDate { get; set; }

        public virtual UserDTO User { get; set; }
        public int UserId { get; set; }

        public virtual CategoryDTO Category { get; set; }
        public int CategoryId { get; set; }

        public virtual ICollection<LikeDTO> Likes { get; set; }
    }
}
