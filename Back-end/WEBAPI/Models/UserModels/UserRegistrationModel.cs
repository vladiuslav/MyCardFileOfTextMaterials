using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WEBAPI.Models
{
    public class UserRegistrationModel
    {
        [Required(ErrorMessage = "Wrong email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Wrong nickname")]
        [StringLength(40)]
        public string NickName { get; set; }

        [Required(ErrorMessage = "Wrong password")]
        [DataType(DataType.Password)]
        [StringLength(40, MinimumLength = 7)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Wrong check password")]
        public string ConfirmPassword { get; set; }
    }
}
