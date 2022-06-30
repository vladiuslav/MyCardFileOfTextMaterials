using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WEBAPI.Models
{
    public class CardInfoModel
    {
        public int Id;
        public string Title { get; set; }
        public string Text { get; set; }
        public int NumberOfLikes { get; set; }
        public int UserId { get; set; }
        public int CategoryId { get; set; }
        public string UserName { get; set; }
        public string CategoryName { get; set; }
    }
}
