using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DLL.Entities
{
    public class Card
    {
        public int ID;
        [Required]
        public string Title { get; set; }
        [Required]
        [Column(TypeName ="ntext")]
        public string Text { get; set; }
        
        public int NumberOfLikes { get; set; }//set here 0 like standart value

        public virtual User User { get; set; }
        public int UserID { get; set; }

        public virtual Category Category { get; set; }
        public int CategoryID { get; set; }

    }
}
