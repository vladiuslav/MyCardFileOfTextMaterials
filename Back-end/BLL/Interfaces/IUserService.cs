using BLL.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Interfaces
{
    public interface IUserService
    {
        void CreateUser(UserDTO userDto);
        void ChangeUser(UserDTO userDto);
        UserDTO GetUser(int id);
        UserDTO GetUserByNickName(string nickname);
        UserDTO GetUserByEmail(string email);
        IEnumerable<UserDTO> GetUsers();
        IEnumerable<UserDTO> GetUsersRange(int []usersId);
        void Dispose();
    }
}
