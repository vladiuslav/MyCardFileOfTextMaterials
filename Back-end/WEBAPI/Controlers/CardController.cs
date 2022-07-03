
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
        private ICategoryService _categoryService;
        public CardController(IMapper mapper, ICardService cardService, IUserService userService, ICategoryService categoryService)
        {
            _mapper = mapper;
            this._cardService = cardService;
            this._userService = userService;
            this._categoryService = categoryService;
        }

        [HttpGet]
        public IEnumerable<CardInfoModel> Get()
        {
            var cards = _mapper.Map<IEnumerable<CardInfoModel>>(_cardService.GetCards());
            var userNames = _cardService.UserNames();
            var categoriesNames = _cardService.CategoryNames();
            foreach (var item in cards)
            {
                item.CategoryName = categoriesNames[item.Id];
                item.UserName = userNames[item.Id];
            }
            return cards;
        }
        [HttpPost("GetCardsByCategory")]
        public IActionResult GetCardsByCategory(CategoryInfoModel category)
        {
            var cards = _mapper.Map<IEnumerable<CardInfoModel>>(_cardService.GetCardsByCategory(category.Name));
            var userNames = _cardService.UserNames();
            var categoriesNames = _cardService.CategoryNames();
            foreach (var item in cards)
            {
                item.CategoryName = categoriesNames[item.Id];
                item.UserName = userNames[item.Id];
            }
            return new JsonResult(cards);
        }
        [HttpGet("{id}")]
        public CardInfoModel Get(int id)
        {
            var card = _mapper.Map<CardInfoModel>(_cardService.GetCard(id));
            return card;
        }

        [Authorize(Roles = "user,admin")]
        [HttpPost("Create")]
        public IActionResult Post(CardCreationModel card)
        {
            int userId = _userService.GetUserByEmail(User.Identity.Name).Id;
            CardDTO cardDTO = _mapper.Map<CardDTO>(card);
            cardDTO.UserId = userId;
            cardDTO.CategoryId = _categoryService.GetCategoryByName(card.CategoryName).Id;


            _cardService.CreateCard(cardDTO);
            CardDTO resultCard = _cardService.GetCardByTitle(cardDTO.Title);
                
            return new JsonResult(resultCard);
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
            if (_cardService.GetCreatorIdByCardId(cardDTO.Id) == userId)
            {
                cardDTO.UserId = _userService.GetUserByEmail(User.Identity.Name).Id;

                _cardService.ChangeCard(_mapper.Map<CardDTO>(card));
                return Ok();
            }
            else{
                return BadRequest();
            }
        }

        [Authorize(Roles = "user,admin")]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _cardService.DeleteCard(id);
            return Ok();
        }
    }
}
