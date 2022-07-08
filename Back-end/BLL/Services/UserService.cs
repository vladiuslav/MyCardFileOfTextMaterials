using AutoMapper;
using BLL.DTO;
using BLL.Interfaces;
using BLL.MapperConfigurations;
using DLL;
using DLL.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BLL.Services
{
    /// <summary>
    /// Implementaion of intarface user service for working with users.
    /// </summary>
    public class UserService : IUserService
    {

        private EFUnitOfWork _unitOfWork;
        private Mapper _mapper;
        /// <summary>
        /// Constructor for creating mapper and for unit of work. 
        /// </summary>
        /// <param name="connectionString">String for connecting to data base</param>
        public UserService(string connectionString)
        {
            this._unitOfWork = new EFUnitOfWork(connectionString);

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<ConfigurationProfile>();
            });
            this._mapper = new Mapper(config);
        }
        public async Task CreateUser(UserDTO userDto)
        {
            await _unitOfWork.Users.Create(_mapper.Map<User>(userDto));
            await _unitOfWork.SaveAsync();
        }
        public async Task ChangeUser(UserDTO userDto)
        {
            _unitOfWork.Users.Update(_mapper.Map<User>(userDto));
            await _unitOfWork.SaveAsync();
        }

        public async Task Dispose()
        {
            await _unitOfWork.SaveAsync();
            _unitOfWork.Dispose();
        }

        public async Task<UserDTO> GetUser(int id)
        {
            return _mapper.Map<UserDTO>(await _unitOfWork.Users.GetAsync(id));
        }
        public UserDTO GetUserByNickName(string nickname)
        {
            return _mapper.Map<UserDTO>(_unitOfWork.Users.GetAll().FirstOrDefault(user => user.NickName == nickname));
        }

        public UserDTO GetUserByEmail(string email)
        {
            var user = _mapper.Map<UserDTO>(_unitOfWork.Users.GetAll().FirstOrDefault(user => user.Email == email));
            return user;
        }

        public IEnumerable<UserDTO> GetUsers()
        {
            return _mapper.Map<IEnumerable<UserDTO>>(_unitOfWork.Users.GetAll());
        }

        public IEnumerable<UserDTO> GetUsersRange(int[] usersId)
        {
            List<UserDTO> users = new List<UserDTO>();
            IEnumerable<UserDTO> allUsers = GetUsers();
            foreach (var item in allUsers)
            {
                foreach (int id in usersId)
                {
                    if (item.Id == id)
                    {
                        users.Add(item);
                        break;
                    }
                }
            }
            return users;
        }

    }
}
