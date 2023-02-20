using System.Threading.Tasks;

using CeremonicBackend.DB.Relational;
using CeremonicBackend.Repositories.Interfaces;

namespace CeremonicBackend.Services
{
    public class ProviderCreatorService : UserCreatorService
    {
        public ProviderCreatorService(IUnitOfWork uow) : base(uow) { }

        public override async Task<dynamic> Create()
        {
            UserEntity user = await CreateUserTables();

            await CreateUserMongoCollections(user);

            return new
            {
                Role = "Provider",
            };
        }

        public override async Task<dynamic> CreateUserMongoCollections(UserEntity user)
        {
            return new { };
        }
    }
}
