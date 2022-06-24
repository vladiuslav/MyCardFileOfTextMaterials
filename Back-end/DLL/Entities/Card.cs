﻿using DLL.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DLL.Entities
{
    public class Card : IEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }
        [Required]
        [Column(TypeName ="ntext")]
        public string Text { get; set; }

        [DefaultValue(0)]
        public int NumberOfLikes { get; set; }

        public virtual User User { get; set; }
        public int UserId { get; set; }

        public virtual Category Category { get; set; }
        public int CategoryId { get; set; }

    }
}
