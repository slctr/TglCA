using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using TglCA.Bll.Interfaces.IServices;
using TglCA.Dal.Interfaces.Entities.Identity;
using TglCA.Dal.Interfaces.IRepositories;
using TglCA.Dal.Interfaces.IUnitOfWork;

namespace TglCA.Bll.Identity
{
    public class RoleService : RoleManager<Role>, IRoleService
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IUnitOfWork _unitOfWork;
        public RoleService(IRoleStore<Role> store, 
            IEnumerable<IRoleValidator<Role>> roleValidators, 
            ILookupNormalizer keyNormalizer, 
            IdentityErrorDescriber errors, 
            ILogger<RoleManager<Role>> logger,
            IRoleRepository repository,
            IUnitOfWork unitOfWork) : base(store, roleValidators, keyNormalizer, errors, logger)
        {
            _roleRepository = repository;
            _unitOfWork = unitOfWork;
        }
    }
}
