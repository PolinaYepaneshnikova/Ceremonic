using CeremonicBackend.Repositories.Interfaces;
using CeremonicBackend.Services.Interfaces;
using CeremonicBackend.WebApiModels;
using System.Threading.Tasks;

namespace CeremonicBackend.Services
{
    public class UserService : IUserService
    {
        protected IUnitOfWork _UoW { get; set; }
        public UserService(IUnitOfWork uow)
        {
            _UoW = uow;
        }



        public Task<UserApiModel> GetUserByEmail(string email)
        {
            throw new System.NotImplementedException();
        }
    }
}
