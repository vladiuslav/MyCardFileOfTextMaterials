using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using DLL.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;

namespace DLL.Entities
{
    public class Like : IEntity
    {
        [Key]
        public int Id { get; set; }
        public virtual User User { get; set; }
        public int UserId { get; set; }
        public virtual Card Card { get; set; }
        public int CardId { get; set; }
    }
}
