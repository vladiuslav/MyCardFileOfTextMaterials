using System;
using System.Collections.Generic;

namespace BLL.DTO
{
    /// <summary>
    /// User DTO (data context object) used working with bll, for mapping card to data logic layer.
    /// </summary>
    public class UserDTO
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string NickName { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public DateTime RegistrationDate { get; set; }
        public virtual ICollection<CardDTO> Cards { get; set; }
        public virtual ICollection<LikeDTO> Likes { get; set; }
        public UserDTO()
        {
            this.Cards = new List<CardDTO>();
            this.Likes = new List<LikeDTO>();
        }
    }
}
