using System.Threading.Tasks;
using CeremonicBackend.DB.Mongo;
using CeremonicBackend.DB.Relational;
using CeremonicBackend.Mappings;
using CeremonicBackend.Repositories.Interfaces;
using CeremonicBackend.Services.Interfaces;
using CeremonicBackend.WebApiModels;

namespace CeremonicBackend.Services
{
    public class UserService : IUserService
    {
        protected IUnitOfWork _UoW { get; set; }
        public UserService(IUnitOfWork uow)
        {
            _UoW = uow;
        }



        public async Task<UserApiModel> Get(string email)
            => (await _UoW.UserRepository.GetByEmail(email)).ToUserApiModel();

        public async Task<string> GetRoleByEmail(string email)
        {
            UserEntity user = await _UoW.UserRepository.GetByEmail(email);

            if (await _UoW.WeddingRepository.GetById(user.Id) is not null)
            {
                return "User";
            }

            if (await _UoW.ProviderRepository.GetById(user.Id) is ProviderEntity provider)
            {
                return (await _UoW.ServiceRepository.GetById(provider.ServiceId)).RelationalServiceEntity.Name;
            }

            return "undefined";
        }
    }
}
