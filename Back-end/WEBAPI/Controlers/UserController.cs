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
using System.Threading.Tasks;
using WEBAPI.Models;

namespace WEBAPI.Controlers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IMapper _mapper;
        private IUserService _userService;
        public UserController(IMapper mapper, IUserService userService)
        {
            _mapper = mapper;
            this._userService = userService;
        }

        [Authorize(Roles = "user,admin")]
        [HttpGet]
        public UserInfoModel Get()
        {
            var user = _mapper.Map<UserInfoModel>(_userService.GetUserByEmail(User.Identity.Name)); 
            return user;
        }

        [HttpGet("name/{id}")]
        public async Task<IActionResult> GetNameAsync(int id)
        {
            var user = await _userService.GetUser(id);
            if(user is null)
            {
                return new NotFoundResult();
            }
            return new JsonResult(_mapper.Map<UserShortInfoModel>(user));
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync(UserRegistrationModel user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            if (_userService.GetUserByEmail(user.Email) is null && _userService.GetUserByNickName(user.NickName) is null)
            {
                UserDTO userdto = _mapper.Map<UserDTO>(user);
                userdto.Role = "user";
                await _userService.CreateUser(userdto);
                return Ok();
            }
            else
            {
                return new ConflictResult();
            }

        }

        [Authorize(Roles = "user,admin")]
        [HttpPut("changeNickname")]
        public async Task<IActionResult> PutNicknameAsync([FromBody] string value)
        {
            UserDTO userBefore = _userService.GetUserByEmail(User.Identity.Name);

            if (String.IsNullOrEmpty(value) || value.Length < 4 || value.Length > 40)
            {
                return BadRequest();
            }
            var userWithNickName = _userService.GetUserByNickName(value);
            if (userWithNickName is null)
            { }
            else if (userWithNickName.Id != userBefore.Id)
            {
                return new ConflictResult();
            }

            UserDTO userdto = new UserDTO
            {
                NickName = value,
                Role = userBefore.Role,
                Id = userBefore.Id,
                Email = userBefore.Email,
                Password = userBefore.Password
            };
            await _userService.ChangeUser(userdto);
            return Ok();
        }

        [Authorize(Roles = "user,admin")]
        [HttpPut("changeEmail")]
        public async Task<IActionResult> PutEmailAsync([FromBody] string value)
        {
            UserDTO userBefore = _userService.GetUserByEmail(User.Identity.Name);

            if (String.IsNullOrEmpty(value) || !value.Contains('@') || value.Length > 500)
            {
                return BadRequest();
            }

            var userWithEmail = _userService.GetUserByEmail(value);
            if (userWithEmail is null)
            { }
            else if (userWithEmail.Id != userBefore.Id)
            {
                return new ConflictResult();
            }

            UserDTO userdto = new UserDTO
            {
                NickName = userBefore.NickName,
                Role = userBefore.Role,
                Id = userBefore.Id,
                Email = value,
                Password = userBefore.Password
            };
            await _userService.ChangeUser(userdto);
            return Ok();
        }

        [Authorize(Roles = "user,admin")]
        [HttpPut("changePassword")]
        public async Task<IActionResult> PutPasswordAsync([FromBody] string value)
        {
            if (String.IsNullOrEmpty(value) || value.Length < 8 || value.Length > 100)
            {
                return BadRequest();
            }

            UserDTO userBefore = _userService.GetUserByEmail(User.Identity.Name);

            UserDTO userdto = new UserDTO
            {
                NickName = userBefore.NickName,
                Role = userBefore.Role,
                Id = userBefore.Id,
                Email = userBefore.Email,
                Password = value
            };
            await _userService.ChangeUser(userdto);
            return Ok();
        }

        [HttpPost("login")]
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
                return BadRequest();
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

            var userInfo = _userService.GetUserByEmail(identity.Name);
            var response = new
            {
                access_token = encodedJwt,
                email = identity.Name,
                userId = userInfo.Id,
                role = userInfo.Role
            };

            return new JsonResult(response);
        }

        private ClaimsIdentity GetIdentity(string email, string password)
        {
            UserDTO user = _userService.GetUsers().FirstOrDefault(user => user.Email == email && user.Password == password);// part with getUsers.firstOrdefault must to be in bll. 
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
