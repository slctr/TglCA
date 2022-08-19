using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using TglCA.Bll.Interfaces.IServices;
using TglCA.Dal.Interfaces.Entities.Identity;
using TglCA.Dal.Interfaces.IRepositories;
using TglCA.Dal.Interfaces.IUnitOfWork;

namespace TglCA.Bll.Identity
{
    public class UserService : UserManager<User>, IUserService
    {
        private readonly IUserRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        public UserService(IUserStore<User> store, 
            IOptions<IdentityOptions> optionsAccessor, 
            IPasswordHasher<User> passwordHasher, 
            IEnumerable<IUserValidator<User>> userValidators, 
            IEnumerable<IPasswordValidator<User>> passwordValidators, 
            ILookupNormalizer keyNormalizer, 
            IdentityErrorDescriber errors, 
            IServiceProvider services, 
            ILogger<UserManager<User>> logger,
            IUserRepository repository,
            IUnitOfWork unitOfWork) : base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services, logger)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }
    }
}
