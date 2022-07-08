using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.DTO
{
    public class LikeDTO
    {
        public int Id { get; set; }
        public bool IsDislike { get; set; }
        public virtual UserDTO User { get; set; }
        public int UserId { get; set; }
        public virtual CardDTO Card { get; set; }
        public int CardId { get; set; }
    }
}
