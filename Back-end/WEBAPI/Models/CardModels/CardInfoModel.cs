﻿using System;

namespace WEBAPI.Models
{
    public class CardInfoModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public DateTime CreationDate { get; set; }
        public int Likes { get; set; }
        public int DisLikes { get; set; }
        public int UserId { get; set; }
        public int CategoryId { get; set; }
        public string UserName { get; set; }
        public string CategoryName { get; set; }
    }
}
