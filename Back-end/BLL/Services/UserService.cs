using BLL.Interfaces;
using BLL.DTO;
using BLL.MapperConfigurations;
using System;
using System.Linq;
using System.Collections.Generic;
using AutoMapper;
using DLL.Entities;
using DLL;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class UserService : IUserService
    {
        
        private EFUnitOfWork _unitOfWork;
        private Mapper _mapper;
        public UserService(string connectionString)
        {
            this._unitOfWork = new EFUnitOfWork(connectionString); 

            var config = new MapperConfiguration(cfg => {
                cfg.AddProfile<ConfigurationProfile>();
                });
            this._mapper = new Mapper(config);
        }
        public async Task CreateUser(UserDTO userDto)
        {
            _unitOfWork.Users.Create(_mapper.Map<User>(userDto));
            await _unitOfWork.SaveAsync();
        }
        public async Task ChangeUser(UserDTO userDto)
        {
            _unitOfWork.Users.Update(_mapper.Map<User>(userDto));
            await _unitOfWork.SaveAsync();
        }

        public async void Dispose()
        {
            await _unitOfWork.SaveAsync();
            _unitOfWork.Dispose();
        }

        public async Task<UserDTO> GetUser(int id)
        {
            return _mapper.Map<UserDTO>(await _unitOfWork.Users.GetByIdAsync(id));
        }
        public async Task<UserDTO> GetUserByNickName(string nickname)
        {
            return _mapper.Map<UserDTO>(await _unitOfWork.Users.FirstOrDefaultAsync(user=>user.NickName==nickname));
        }

        public async Task<UserDTO> GetUserByEmail(string email)
        {
            return _mapper.Map<UserDTO>(await _unitOfWork.Users.FirstOrDefaultAsync(user => user.Email == email));
        }

        public async Task<IEnumerable<UserDTO>> GetUsersAsync()
        {
            return _mapper.Map<IEnumerable<UserDTO>>(await _unitOfWork.Users.GetAllAsync());
        }

        public async Task<IEnumerable<UserDTO>> GetUsersRange(int[] usersId)
        {
            List<UserDTO> users = new List<UserDTO>();
            IEnumerable<UserDTO> allUsers = await GetUsersAsync();
            foreach (var item in allUsers)
            {
                foreach (int id in usersId)
                {
                    if (item.Id==id)
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
