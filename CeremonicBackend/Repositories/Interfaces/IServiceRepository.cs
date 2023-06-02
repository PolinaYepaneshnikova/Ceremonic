using System.Threading.Tasks;

using CeremonicBackend.DB.Relational;

namespace CeremonicBackend.Repositories.Interfaces
{
    public interface IServiceRepository :  IBaseRepository<GeneralServiceEntity, int>
    {
        Task<GeneralServiceEntity> GetByName(string name);
    }
}
