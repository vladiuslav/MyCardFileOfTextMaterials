using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WEBAPI.Models
{
    public class CategoryInfoModel
    {
        [Required(ErrorMessage = "Wrong id")]
        public string Id { get; set; }

        [Required(ErrorMessage = "Wrong category name")]
        [StringLength(40, MinimumLength = 3)]
        public string Name { get; set; }
    }
}
