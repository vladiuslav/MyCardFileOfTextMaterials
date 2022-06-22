using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WEBAPI.Models
{
    public class UserUpdateModel
    {
        [Required(ErrorMessage = "No userId specified")]
        public int Id { get; set; }

        [Required(ErrorMessage = "No email specified")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "No nickname specified")]
        [StringLength(40)]
        public string NickName { get; set; }

        [Required(ErrorMessage = "No password specified")]
        [DataType(DataType.Password)]
        [StringLength(40, MinimumLength = 7)]
        public string Password { get; set; }
    }
}
