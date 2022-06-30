using AutoMapper;
using BLL.DTO;
using BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using WEBAPI.Models;

namespace WEBAPI.Controlers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IMapper _mapper;
        private IUserService _userService;
        public UserController(IMapper mapper,IUserService userService)
        {
            _mapper = mapper;
            this._userService = userService;
        }

        // GET api/<UserController>/account
        [Authorize(Roles = "user,admin")]
        [HttpGet("/account")]
        public UserInfoModel Get()
        {
            return _mapper.Map<UserInfoModel>(_userService.GetUserByEmail(User.Identity.Name));
        }

        [HttpGet("name/{id}")]
        public UserShortInfoModel GetName(int id)
        {
            return _mapper.Map<UserShortInfoModel>(_userService.GetUser(id));
        }

        // POST api/<UserController>
        [HttpPost]
        public IActionResult Post(UserRegistrationModel user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            if (_userService.GetUserByEmail(user.Email) is null && _userService.GetUserByNickName(user.NickName) is null)
            {
                UserDTO userdto = _mapper.Map<UserDTO>(user);
                userdto.Role = "user";
                _userService.CreateUser(userdto);
                return Ok();
            }
            else
            {
                return BadRequest();
            }
            
        }

        // PUT api/<UserController>
        [Authorize(Roles = "user,admin")]
        [HttpPut]
        public IActionResult Put(UserUpdateModel user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            UserDTO userBefore = _userService.GetUserByEmail(User.Identity.Name);
            if ((userBefore.Email == user.Email || _userService.GetUserByEmail(user.Email) is null)
                && (userBefore.NickName == user.NickName || _userService.GetUserByNickName(user.NickName) == null))
            {
                UserDTO userdto = _mapper.Map<UserDTO>(user);
                userdto.Role = userBefore.Role;
                userdto.Id = userBefore.Id;
                _userService.ChangeUser(userdto);
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        //POST api/<UserController>/login
        [HttpPost("/login")]
        public IActionResult Login(UserLoginModel user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            string email = user.Email, password = user.Password;
            var identity = GetIdentity(email, password);
            if (identity == null)
            {
                return BadRequest(new { errorText = "Invalid username or password." });
            }

            var now = DateTime.UtcNow;
            var jwt = new JwtSecurityToken(
                    issuer: AuthOptions.ISSUER,
                    audience: AuthOptions.AUDIENCE,
                    notBefore: now,
                    claims: identity.Claims,
                    expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                    signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            var response = new
            {
                access_token = encodedJwt,
                username = identity.Name
            };

            JsonResult json = new JsonResult(response);

            return json;
        }

        private ClaimsIdentity GetIdentity(string email, string password)
        {
            UserDTO user = _userService.GetUsers().FirstOrDefault(user=>user.Email==email&&user.Password==password);// part with getUsers.firstOrdefault must to be in bll. 
            if (user != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email),
                    new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role)
                };
                ClaimsIdentity claimsIdentity =
                new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                    ClaimsIdentity.DefaultRoleClaimType);
                return claimsIdentity;
            }

            return null;
        }
    }
}
