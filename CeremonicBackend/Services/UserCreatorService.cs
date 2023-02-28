using System.Threading.Tasks;

using CeremonicBackend.DB.Relational;
using CeremonicBackend.Repositories.Interfaces;
using CeremonicBackend.Services.Interfaces;
using CeremonicBackend.WebApiModels;

namespace CeremonicBackend.Services
{
    public abstract class UserCreatorService : IUserCreatorService
    {
        public RegistrationApiModel RegistrationModel { get; set; }

        protected IUnitOfWork _UoW { get; set; }
        public UserCreatorService(IUnitOfWork uow)
        {
            _UoW = uow;
        }

        public abstract Task<dynamic> Create();

        public virtual async Task<UserEntity> CreateUserTables()
        {
            UserEntity user = new UserEntity()
            {
                FirstName = RegistrationModel.FirstName,
                LastName = RegistrationModel.LastName,
                LoginInfo = new UserLoginInfoEntity()
                {
                    Email = RegistrationModel.Email,
                    PasswordHash = AccountService.HashPassword(RegistrationModel.Password),
                },
            };
            user = await _UoW.UserRepository.Add(user);
            await _UoW.SaveChanges();

            return user;
        }
        
        public abstract Task<dynamic> CreateUserMongoCollections(UserEntity user);
    }
}
