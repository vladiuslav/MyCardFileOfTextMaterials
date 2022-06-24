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
        public CardController(IMapper mapper, ICardService cardService)
        {
            _mapper = mapper;
            this._cardService = cardService;
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
        public void Post(CardCreationModel card)
        {
            _cardService.CreateCard(_mapper.Map<CardDTO>(card));
        }

        // PUT api/<CardController>/5
        [Authorize(Roles = "user,admin")]
        [HttpPut("{id}")]
        public void Put(CardCreationModel card)
        {
            _cardService.ChangeCard(_mapper.Map<CardDTO>(card));
        }

        // DELETE api/<CardController>/5
        [Authorize(Roles = "user,admin")]
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _cardService.DeleteCard(id);
        }
    }
}
