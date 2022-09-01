using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TglCA.Bll.Interfaces.Entities;
using TglCA.Bll.Interfaces.IServices;
using TglCA.Dal.Interfaces.Entities;
using TglCA.Dal.Interfaces.IRepositories;

namespace TglCA.Bll.Services
{
    public class CurrencyService : ICurrencyService
    {
        private readonly ICurrencyRepository _currencyRepository;

        public CurrencyService(ICurrencyRepository repository)
        {
            _currencyRepository = repository;
        }
        public void Create(BllCurrency entity)
        {
            Currency currency = new Currency()
            {
                Id = entity.Id,
                Img = entity.Img,
                Name = entity.Name,
                Symbol = entity.Symbol
            };
        }

        public void Update(BllCurrency entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(BllCurrency entity)
        {
            throw new NotImplementedException();
        }

        public BllCurrency GetById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<BllCurrency> GetAll()
        {
            throw new NotImplementedException();
        }

        public void CreateOrUpdate(BllCurrency entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<BllCurrency> GetAllByMarketCap()
        {
            throw new NotImplementedException();
        }
    }
}
