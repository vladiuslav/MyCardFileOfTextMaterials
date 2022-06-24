using BLL.Interfaces;
using BLL.DTO;
using BLL.MapperConfigurations;
using System;
using System.Linq;
using System.Collections.Generic;
using AutoMapper;
using DLL.Entities;
using DLL;

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
        public void CreateUser(UserDTO userDto)
        {
            _unitOfWork.Users.Create(_mapper.Map<User>(userDto));
            _unitOfWork.Save();
        }
        public void ChangeUser(UserDTO userDto)
        {
            _unitOfWork.Users.Update(_mapper.Map<User>(userDto));
            _unitOfWork.Save();
        }

        public void Dispose()
        {
            _unitOfWork.Save();
            _unitOfWork.Dispose();
        }

        public UserDTO GetUser(int id)
        {
            return _mapper.Map<UserDTO>(_unitOfWork.Users.Get(id));
        }
        public UserDTO GetUserByNickName(string nickname)
        {
            return _mapper.Map<UserDTO>(_unitOfWork.Users.GetAll().FirstOrDefault(user => user.NickName == nickname));
        }

        public UserDTO GetUserByEmail(string email)
        {
            return _mapper.Map<UserDTO>(_unitOfWork.Users.GetAll().FirstOrDefault(user=>user.Email==email));
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
