
using AutoMapper;
using BLL.DTO;
using BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
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
        public async System.Threading.Tasks.Task<IEnumerable<CardInfoModel>> GetAsync()
        {
            var cards = _mapper.Map<IEnumerable<CardInfoModel>>(_cardService.GetCards());
            var userNames = await _cardService.UserNamesAsync();
            var categoriesNames = await _cardService.CategoryNamesAsync();
            foreach (var item in cards)
            {
                item.CategoryName = categoriesNames[item.Id];
                item.UserName = userNames[item.Id];
            }
            return cards;
        }
        [HttpPost("GetCardsByCategory")]
        public async System.Threading.Tasks.Task<IActionResult> GetCardsByCategoryAsync(CategoryInfoModel category)
        {
            if (_categoryService.GetCategoryByName(category.Name) is null)
            {
                return BadRequest();
            }
            var cards = _mapper.Map<IEnumerable<CardInfoModel>>(_cardService.GetCardsByCategory(category.Name));
            var userNames = await _cardService.UserNamesAsync();
            var categoriesNames = await _cardService.CategoryNamesAsync();
            foreach (var item in cards)
            {
                item.CategoryName = categoriesNames[item.Id];
                item.UserName = userNames[item.Id];
            }
            return new JsonResult(cards);


        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            if (await _cardService.GetCardAsync(id) is null)
            {
                return BadRequest();
            }
            var card = _mapper.Map<CardInfoModel>(await _cardService.GetCardAsync(id));
            return new JsonResult(card);
        }

        [Authorize(Roles = "user,admin")]
        [HttpPost("Create")]
        public async Task<IActionResult> PostAsync(CardCreationModel card)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            int userId = _userService.GetUserByEmail(User.Identity.Name).Id;
            CardDTO cardDTO = _mapper.Map<CardDTO>(card);
            cardDTO.UserId = userId;
            cardDTO.CategoryId = _categoryService.GetCategoryByName(card.CategoryName).Id;


            await _cardService.CreateCard(cardDTO);
            CardDTO resultCard = _cardService.GetCardByTitle(cardDTO.Title);

            return new JsonResult(resultCard);
        }

        // PUT api/<CardController>/5
        [Authorize(Roles = "user,admin")]
        [HttpPut]
        public async Task<IActionResult> PutAsync(CardUpdateModel card)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            int userId = _userService.GetUserByEmail(User.Identity.Name).Id;
            CardDTO cardDTO = _mapper.Map<CardDTO>(card);
            if (await _cardService.GetCreatorIdByCardIdAsync(cardDTO.Id) == userId)
            {
                cardDTO.UserId = _userService.GetUserByEmail(User.Identity.Name).Id;
                cardDTO.CategoryId = _categoryService.GetCategoryByName(card.categoryName).Id;
                await _cardService.ChangeCard(_mapper.Map<CardDTO>(card));
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        [Authorize(Roles = "user,admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            await _cardService.DeleteCard(id);
            return Ok();
        }
    }
}
