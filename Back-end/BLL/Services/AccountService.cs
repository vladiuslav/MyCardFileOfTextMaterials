using BLL.Interfaces;
using BLL.DTO;
using BLL.MapperConfigurations;
using System;
using System.Collections.Generic;
using AutoMapper;
using DLL.Entities;
using DLL;

namespace BLL.Services
{
    public class AccountService : IUserService
    {
        
        private EFUnitOfWork unitOfWork;
        private Mapper mapper;
        public AccountService(string connectionString)
        {
            this.unitOfWork = new EFUnitOfWork(connectionString); 

            var config = new MapperConfiguration(cfg => {
                cfg.AddProfile<CardConfigurationProfile>();
                cfg.AddProfile<UserConfigurationProfile>();
                cfg.AddProfile<CategoryConfigurationProfile>();
                });
            this.mapper = new Mapper(config);
        }
        public void CreateUser(UserDTO userDto)
        {
            unitOfWork.Users.Create(mapper.Map<User>(userDto));
            unitOfWork.Save();
        }

        public void Dispose()
        {
            unitOfWork.Save();
            unitOfWork.Dispose();
        }

        public UserDTO GetUser(int id)
        {
            return mapper.Map<UserDTO>(unitOfWork.Users.Get(id));
        }

        public IEnumerable<UserDTO> GetUsers()
        {
            return mapper.Map<IEnumerable<UserDTO>>(unitOfWork.Users.GetAll());
        }
    }
}
