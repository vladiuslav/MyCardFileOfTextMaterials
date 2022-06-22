using AutoMapper;
using BLL.DTO;
using BLL.Interfaces;
using BLL.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEBAPI.Models;

namespace WEBAPI.Controlers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IMapper _mapper;
        private IUserService userService;
        public UserController(IMapper mapper,IUserService userService)
        {
            _mapper = mapper;
            this.userService = userService;
        }
        // GET: api/<UserController>
        [HttpGet]
        public IEnumerable<UserShortInfoModel> Get(int start,int end)
        {
            return _mapper.Map<IEnumerable<UserShortInfoModel>>(userService.GetUsersRange(start,end));
        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public UserAllInfoModel Get(int id)
        {
            return _mapper.Map<UserAllInfoModel>(userService.GetUser(id));
        }

        // POST api/<UserController>
        [HttpPost]
        public void Post(UserRegistrationModel user)
        {
            userService.CreateUser(_mapper.Map<UserDTO>(user));
        }

        // PUT api/<UserController>
        [HttpPut]
        public void Put(UserUpdateModel user)
        {
            userService.ChangeUser(_mapper.Map<UserDTO>(user));
        }
    }
}
