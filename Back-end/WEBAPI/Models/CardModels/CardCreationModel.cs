using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WEBAPI.Models
{
    public class CardCreationModel
    {
        public string Title { get; set; }
        public string Text { get; set; }
        public string CategoryName { get; set; }
    }
}
