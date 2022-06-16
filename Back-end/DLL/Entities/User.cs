using DLL.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DLL.Entities
{
    public class User : IEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Email { get; set; }
        [Required]
        [StringLength(40)]
        public string NickName { get; set; }
        [Required]
        public string Password { get; set; }
        public virtual ICollection<Card> Cards { get; set; }

        public User() {
            this.Cards = new List<Card>();
        }
    }
}
