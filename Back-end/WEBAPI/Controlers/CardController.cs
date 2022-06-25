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
    public class CardController : ControllerBase
    {
        private IMapper _mapper;
        private ICardService _cardService;
        private IUserService _userService;
        public CardController(IMapper mapper, ICardService cardService, IUserService userService)
        {
            _mapper = mapper;
            this._cardService = cardService;
            this._userService = userService;
        }

        // GET: api/<CardController>
        [HttpGet]
        public IEnumerable<CardInfoModel> Get()
        {
            return _mapper.Map<IEnumerable<CardInfoModel>>(_cardService.GetCards());
        }
        // GET: api/<CardController>/
        [HttpGet("/mostLikedCards")]
        public IEnumerable<CardInfoModel> GetMostLikedCards()
        {
            return _mapper.Map<IEnumerable<CardInfoModel>>(_cardService.GetMostLikedCards());
        }
        // GET: api/<CardController>
        [HttpGet("/GetCardsByCategory/{id}")]
        public IEnumerable<CardInfoModel> GetCardsByCategory(int id)
        {
            return _mapper.Map<IEnumerable<CardInfoModel>>(_cardService.GetCardsByCategory(id));
        }

        // GET api/<CardController>/5
        [HttpGet("{id}")]
        public CardInfoModel Get(int id)
        {
            return _mapper.Map<CardInfoModel>(_cardService.GetCard(id));
        }

        // POST api/<CardController>
        [Authorize(Roles ="user,admin")]
        [HttpPost]
        public IActionResult Post(CardCreationModel card)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            int userId = _userService.GetUserByEmail(User.Identity.Name).Id;
            CardDTO cardDTO = _mapper.Map<CardDTO>(card);
            if (_cardService.GetCardCreatorId(cardDTO.Id)==userId)
            {
                cardDTO.UserId = userId;

                _cardService.CreateCard(cardDTO);
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        // PUT api/<CardController>/5
        [Authorize(Roles = "user,admin")]
        [HttpPut("{id}")]
        public IActionResult Put(CardUpdateModel card)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            int userId = _userService.GetUserByEmail(User.Identity.Name).Id;
            CardDTO cardDTO = _mapper.Map<CardDTO>(card);
            if (_cardService.GetCardCreatorId(cardDTO.Id) == userId)
            {
                cardDTO.UserId = _userService.GetUserByEmail(User.Identity.Name).Id;

                _cardService.ChangeCard(_mapper.Map<CardDTO>(card));
                return Ok();
            }
            else{
                return BadRequest();
            }
        }

        // DELETE api/<CardController>/5
        [Authorize(Roles = "user,admin")]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _cardService.DeleteCard(id);
            return Ok();
        }
    }
}
