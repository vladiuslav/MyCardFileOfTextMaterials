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
    public class CardService : ICardService
    {

        private EFUnitOfWork unitOfWork;
        private Mapper mapper;
        public CardService(string connectionString)
        {
            this.unitOfWork = new EFUnitOfWork(connectionString);

            var config = new MapperConfiguration(cfg => {
                cfg.AddProfile<CardConfigurationProfile>();
                cfg.AddProfile<UserConfigurationProfile>();
                cfg.AddProfile<CategoryConfigurationProfile>();
            });
            this.mapper = new Mapper(config);
        }
        public void CreateCard(CardDTO cardDto)
        {
            unitOfWork.Cards.Create(mapper.Map<Card>(cardDto));
            unitOfWork.Save();
        }

        public void Dispose()
        {
            unitOfWork.Save();
            unitOfWork.Dispose();
        }

        public CardDTO GetCard(int id)
        {
            return mapper.Map<CardDTO>(unitOfWork.Cards.Get(id));
        }

        public IEnumerable<CardDTO> GetCards()
        {
            return mapper.Map<IEnumerable<CardDTO>>(unitOfWork.Cards.GetAll());
        }
    }
}
