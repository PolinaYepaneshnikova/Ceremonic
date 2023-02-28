using System.Threading.Tasks;
using CeremonicBackend.DB.Mongo;
using CeremonicBackend.DB.Relational;
using CeremonicBackend.Repositories.Interfaces;
using CeremonicBackend.Services.Interfaces;
using CeremonicBackend.WebApiModels;
using Newtonsoft.Json;

namespace CeremonicBackend.Services
{
    public class ProviderCreatorService : UserCreatorService
    {
        public ProviderInfoApiModel ProviderInfo { get; set; }

        protected IProviderService _providerService { get; set; }
        public ProviderCreatorService(IUnitOfWork uow, IProviderService providerService) : base(uow)
        {
            _providerService = providerService;
        }


        public override async Task<dynamic> Create()
        {
            UserEntity user = await CreateUserTables();

            await CreateUserMongoCollections(user);

            ProviderEntity provider = await _UoW.ProviderRepository.GetById(user.Id);

            return new
            {
                Role = (await _UoW.ServiceRepository.GetById(provider.ServiceId)).RelationalServiceEntity.Name,
            };
        }

        public override async Task<dynamic> CreateUserMongoCollections(UserEntity user)
        {
            return new {
                Provider = await _providerService.CreateForUser(user, ProviderInfo),
            };
        }
    }
}
