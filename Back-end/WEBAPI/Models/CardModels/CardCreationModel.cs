using System.ComponentModel.DataAnnotations;

namespace WEBAPI.Models
{
    public class CardCreationModel
    {
        [Required(ErrorMessage = "Wrong title")]
        [DataType(DataType.Text)]
        [StringLength(40, MinimumLength = 5)]
        public string Title { get; set; }
        [Required(ErrorMessage = "Wrong text")]
        public string Text { get; set; }
        [Required(ErrorMessage = "Wrong category name")]
        public string CategoryName { get; set; }
    }
}
