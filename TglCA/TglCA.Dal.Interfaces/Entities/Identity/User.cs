using Microsoft.AspNetCore.Identity;

namespace TglCA.Dal.Interfaces.Entities.Identity;

public class User : IdentityUser<int>
{
    public IEnumerable<Currency> Currencies { get; set; } = new List<Currency>();
}