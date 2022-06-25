using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WEBAPI.Models
{
    public class CategoryCreationModel
    {
        [Required(ErrorMessage = "Wrong category name")]
        [StringLength(40, MinimumLength = 5)]
        public string Name { get; set; }
    }
}
