using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DLL.Entities
{
    public class User
    {
        public int ID { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        [StringLength(40)]
        public string Nickname { get; set; }
        [Required]
        public string Password { get; set; }
        public virtual ICollection<Card> Cards { get; set; }

        public User() {
            this.Cards = new List<Card>();
        }
    }
}
