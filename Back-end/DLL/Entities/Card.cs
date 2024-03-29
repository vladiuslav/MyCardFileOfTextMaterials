﻿using DLL.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DLL.Entities
{
    /// <summary>
    /// Card entity used for creating cards.
    /// </summary>
    public class Card : IEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }
        [Required]
        [Column(TypeName = "ntext")]
        public string Text { get; set; }

        [Required]
        public DateTime CreationDate { get; set; }
        public virtual User User { get; set; }
        public int UserId { get; set; }
        public virtual Category Category { get; set; }
        public int CategoryId { get; set; }
        public virtual ICollection<Like> Likes { get; set; }

        /// <summary>
        /// Create empty card.
        /// </summary>
        public Card()
        {
            this.Likes = new List<Like>();
            this.CreationDate = DateTime.Now;
        }

    }
}
