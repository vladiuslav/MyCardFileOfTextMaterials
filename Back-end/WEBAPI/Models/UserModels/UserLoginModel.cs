﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

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
