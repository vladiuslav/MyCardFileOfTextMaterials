using System.ComponentModel.DataAnnotations;

namespace WEBAPI.Models
{
    public class CategoryCreationModel
    {
        [Required(ErrorMessage = "Wrong category name")]
        [StringLength(40, MinimumLength = 5)]
        public string Name { get; set; }
    }
}
