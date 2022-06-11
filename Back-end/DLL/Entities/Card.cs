using System;
using System.Collections.Generic;
using System.Text;

namespace DLL.Entities
{
    public class Card
    {
        public int CardId;
        public string Title { get; set; }
        public string Text { get; set; }
        public int NumberOfLikes { get; set; }

        public User User { get; set; }
        public int UserId { get; set; }
        public Category Category { get; set; }
        public int CategoryId { get; set; }

    }
}
