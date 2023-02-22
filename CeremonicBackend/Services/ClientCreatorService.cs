using System.Threading.Tasks;

using CeremonicBackend.DB.Relational;
using CeremonicBackend.Repositories.Interfaces;
using CeremonicBackend.Services.Interfaces;

namespace CeremonicBackend.Services
{
    public class ClientCreatorService : UserCreatorService
    {
        protected IWeddingService _weddingService { get; set; }
        public ClientCreatorService(IUnitOfWork uow, IWeddingService weddingService) : base(uow)
        {
            _weddingService = weddingService;
        }

        public override async Task<dynamic> Create()
        {
            UserEntity user = await CreateUserTables();

            await CreateUserMongoCollections(user);

            return new
            {
                Role = "User",
            };
        }

        public override async Task<dynamic> CreateUserMongoCollections(UserEntity user)
        {
            return new
            {
                Wedding = await _weddingService.CreateForUser(user),
            };
        }
    }
}
