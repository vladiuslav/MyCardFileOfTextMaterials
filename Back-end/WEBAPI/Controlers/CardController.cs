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
        public async Task<IEnumerable<CardInfoModel>> Get()
        {
            return _mapper.Map<IEnumerable<CardInfoModel>>(await _cardService.GetCardsAsync());
        }
        // GET: api/<CardController>/
        [HttpGet("/mostLikedCards")]
        public async Task<IEnumerable<CardInfoModel>> GetMostLikedCards()
        {
            return _mapper.Map<IEnumerable<CardInfoModel>>(await _cardService.GetMostLikedCardsAsync());
        }
        // GET: api/<CardController>
        [HttpGet("/GetCardsByCategory/{id}")]
        public async Task<IEnumerable<CardInfoModel>> GetCardsByCategory(int id)
        {
            return _mapper.Map<IEnumerable<CardInfoModel>>(await _cardService.GetCardsByCategoryAsync(id));
        }

        // GET api/<CardController>/5
        [HttpGet("{id}")]
        public async Task<CardInfoModel> Get(int id)
        {
            return _mapper.Map<CardInfoModel>(await _cardService.GetCardAsync(id));
        }

        // POST api/<CardController>
        [Authorize(Roles ="user,admin")]
        [HttpPost]
        public async Task PostAsync(CardCreationModel card)
        {
            await _cardService.CreateCard(_mapper.Map<CardDTO>(card));
        }
       
        // PUT api/<CardController>/5
        [Authorize(Roles = "user,admin")]
        [HttpPut("{id}")]
        public async Task PutAsync(CardCreationModel card)
        {
            await _cardService.ChangeCard(_mapper.Map<CardDTO>(card));
        }

        // DELETE api/<CardController>/5
        [Authorize(Roles = "user,admin")]
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await _cardService.DeleteCardAsync(_cardService.GetCardAsync(id).Result);
        }
    }
}
