﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DLL.Entities
{
    public class User
    {
        public int UserId { get; set; }
        public string Email { get; set; }
        public string Nickname { get; set; }
        public string Password { get; set; }
    }
}
