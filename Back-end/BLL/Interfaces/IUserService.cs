using BLL.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IUserService
    {
        Task CreateUser(UserDTO userDto);
        Task ChangeUser(UserDTO userDto);
        Task<UserDTO> GetUser(int id);
        Task<UserDTO> GetUserByNickName(string nickname);
        Task<UserDTO> GetUserByEmail(string email);
        Task<IEnumerable<UserDTO>> GetUsersAsync();
        Task<IEnumerable<UserDTO>> GetUsersRange(int[] usersId);
        void Dispose();
    }
}
