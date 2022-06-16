using BLL.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Interfaces
{
    public interface IUserService
    {
        void CreateUser(UserDTO orderDto);
        UserDTO GetUser(int id);
        IEnumerable<UserDTO> GetUsers();
        void Dispose();
    }
}
