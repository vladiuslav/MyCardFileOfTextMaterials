using DLL.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DLL.Entities
{
    public class Like : IEntity
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public bool IsDislike { get; set; }
        public virtual User User { get; set; }
        public int UserId { get; set; }
        public virtual Card Card { get; set; }
        public int CardId { get; set; }


    }
}
