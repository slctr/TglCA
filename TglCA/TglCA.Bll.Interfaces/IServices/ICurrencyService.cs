using TglCA.Bll.Interfaces.Entities;
using TglCA.Bll.Interfaces.IServices.Base;

namespace TglCA.Bll.Interfaces.IServices;

public interface ICurrencyService : IService<BllCurrency>
{
    public void CreateOrUpdate(BllCurrency entity);
    public IEnumerable<BllCurrency> GetAllByMarketCap();
}