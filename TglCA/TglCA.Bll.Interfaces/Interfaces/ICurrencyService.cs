using TglCA.Bll.Interfaces.Entities;
using TglCA.Bll.Interfaces.IServices.Base;

namespace TglCA.Bll.Interfaces.Interfaces;

public interface ICurrencyService : IService<BllCurrency>
{
    void CreateOrUpdate(BllCurrency entity);
    IEnumerable<BllCurrency> GetAllByMarketCap();
}