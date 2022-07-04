using System.ComponentModel.DataAnnotations;

namespace WEBAPI.Models
{
    public class UserLoginModel
    {
        [Required(ErrorMessage = "Wrong email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Wrong password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
