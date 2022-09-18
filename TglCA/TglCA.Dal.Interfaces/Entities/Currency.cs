using TglCA.Dal.Interfaces.Entities.Base;
using TglCA.Dal.Interfaces.Entities.Identity;

namespace TglCA.Dal.Interfaces.Entities;

public class Currency : BaseEntity
{
    public string Symbol { get; set; }

    public IEnumerable<User> Users { get; set; } = new List<User>();
}