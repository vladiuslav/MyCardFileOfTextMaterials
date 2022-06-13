using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace DLL.Entities
{
    public class Category
    {
        public string ID { get; set; }
        [Required]
        public string Name { get; set; }

        public virtual ICollection<Card> Cards { get; set; }

        public Category()
        {
            this.Cards = new List<Card>();
        }
    }
}
