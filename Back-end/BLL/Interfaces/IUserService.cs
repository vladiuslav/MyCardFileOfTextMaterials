using BLL.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IUserService
    {
        Task CreateUser(UserDTO userDto);
        Task ChangeUser(UserDTO userDto);
        Task<UserDTO> GetUser(int id);
        UserDTO GetUserByNickName(string nickname);
        UserDTO GetUserByEmail(string email);
        IEnumerable<UserDTO> GetUsers();
        IEnumerable<UserDTO> GetUsersRange(int[] usersId);
        Task Dispose();
    }
}
