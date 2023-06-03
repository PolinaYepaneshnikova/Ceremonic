using System.Threading.Tasks;
using CeremonicBackend.DB.Mongo;
using CeremonicBackend.DB.Relational;
using CeremonicBackend.Mappings;
using CeremonicBackend.Repositories.Interfaces;
using CeremonicBackend.Services.Interfaces;
using CeremonicBackend.WebApiModels;

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

            ProviderApiModel provider = await _providerService.Get(user.Id);

            return new
            {
                Role = provider.ServiceName,
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
