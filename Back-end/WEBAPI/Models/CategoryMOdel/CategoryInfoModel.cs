using System.ComponentModel.DataAnnotations;

namespace WEBAPI.Models
{
    public class CategoryInfoModel
    {
        [Required(ErrorMessage = "Wrong id")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Wrong category name")]
        [StringLength(40, MinimumLength = 3)]
        public string Name { get; set; }
    }
}
