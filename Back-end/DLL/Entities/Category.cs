using DLL.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DLL.Entities
{
    /// <summary>
    /// Category entity used for creating categories.
    /// </summary>
    public class Category : IEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(40)]
        public string Name { get; set; }
        public virtual ICollection<Card> Cards { get; set; }
        /// <summary>
        /// Create empty category.
        /// </summary>
        public Category()
        {
            this.Cards = new List<Card>();
        }
    }
}
