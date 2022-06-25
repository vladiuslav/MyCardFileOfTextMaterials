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

        // GET api/<UserController>/account
        [Authorize(Roles = "user,admin")]
        [HttpGet("/account")]
        public UserInfoModel Get()
        {
            return _mapper.Map<UserInfoModel>(_userService.GetUserByEmail(User.Identity.Name));
        }

        // POST api/<UserController>
        [HttpPost]
        public async Task<IActionResult> PostAsync(UserRegistrationModel user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            if (await _userService.GetUserByEmail(user.Email) is null && await _userService.GetUserByNickName(user.NickName) is null)
            {
                UserDTO userdto = _mapper.Map<UserDTO>(user);
                userdto.Role = "user";
                await _userService.CreateUser(userdto);
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
        public async Task<IActionResult> PutAsync(UserUpdateModel user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            UserDTO userBefore = await _userService.GetUserByEmail(User.Identity.Name);
            if ((userBefore.Email == user.Email || await _userService.GetUserByEmail(user.Email) is null)
                && (userBefore.NickName == user.NickName || await _userService.GetUserByNickName(user.NickName) == null))
            {
                UserDTO userdto = _mapper.Map<UserDTO>(user);
                userdto.Role = userBefore.Role;
                userdto.Id = userBefore.Id;
                await _userService.ChangeUser(_mapper.Map<UserDTO>(user));
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        //POST api/<UserController>/login
        [HttpPost("/login")]
        public async Task<IActionResult> LoginAsync(UserLoginModel user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            string email = user.Email, password = user.Password;
            var identity = await GetIdentityAsync(email, password);
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

        private async Task<ClaimsIdentity> GetIdentityAsync(string email, string password)
        {
            UserDTO user = (await _userService.GetUsersAsync()).FirstOrDefault(user => user.Email == email && user.Password == password);// part with getUsers.firstOrdefault must to be in bll. 
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
