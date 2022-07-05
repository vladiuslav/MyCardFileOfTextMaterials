using System.ComponentModel.DataAnnotations;

namespace WEBAPI.Models
{
    public class CardUpdateModel
    {
        [Required(ErrorMessage = "Wrong id")]
        public int Id { get; set; }
        [Required(ErrorMessage = "Wrong title")]
        [DataType(DataType.Text)]
        [StringLength(40, MinimumLength = 5)]
        public string Title { get; set; }

        [Required(ErrorMessage = "Wrong Text")]
        [DataType(DataType.Text)]
        public string Text { get; set; }

        [Required(ErrorMessage = "Wrong Category")]
        public string categoryName { get; set; }
    }
}
