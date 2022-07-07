using System;

namespace WEBAPI.Models
{
    public class UserInfoModel
    {
        public string NickName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime RegistrationDate { get; set; }
    }
}
