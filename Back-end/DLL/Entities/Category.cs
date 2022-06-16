using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using DLL.Interfaces;

namespace DLL.Entities
{
    public class Category : IEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public virtual ICollection<Card> Cards { get; set; }

        public Category()
        {
            this.Cards = new List<Card>();
        }
    }
}
