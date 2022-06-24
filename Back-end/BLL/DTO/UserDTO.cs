using System.Collections.Generic;

namespace BLL.DTO
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string NickName { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public virtual ICollection<CardDTO> Cards { get; set; }
        public UserDTO() {
            this.Cards = new List<CardDTO>();
        }
    }
}
