using System.Collections.Generic;
using System.Threading.Tasks;

using CeremonicBackend.DB.Mongo;
using CeremonicBackend.Exceptions;
using CeremonicBackend.Repositories.Interfaces;
using CeremonicBackend.Services.Interfaces;
using CeremonicBackend.Mappings;
using CeremonicBackend.WebApiModels;
using System.Linq;

namespace CeremonicBackend.Services
{
    public class FavoriteService : IFavoriteService
    {
        protected IUnitOfWork _UoW { get; set; }
        public FavoriteService(IUnitOfWork uow)
        {
            _UoW = uow;
        }



        public async Task<List<ProviderApiModel>> GetAll(string userEmail)
        {
            WeddingEntity wedding = await _UoW.WeddingRepository.GetByEmail(userEmail);

            if (wedding is null)
            {
                throw new NotFoundAppException($"wedding not found");
            }

            wedding = await wedding.IncludeFavorites(_UoW.ProviderRepository);

            return wedding.MyFavorites
                .Select(async w => await w.ToProviderApiModel(_UoW.ServiceRepository))
                .Select(task => task.Result)
                .ToList();
        }

        public async Task Add(string userEmail, int providerId)
        {
            WeddingEntity wedding = await _UoW.WeddingRepository.GetByEmail(userEmail);

            if (wedding is null)
            {
                throw new NotFoundAppException($"wedding not found");
            }

            wedding.MyFavoritesIds.Add(providerId);

            await _UoW.WeddingRepository.Update(wedding);
        }

        public async Task Delete(string userEmail, int providerId)
        {
            WeddingEntity wedding = await _UoW.WeddingRepository.GetByEmail(userEmail);

            if (wedding is null)
            {
                throw new NotFoundAppException($"wedding not found");
            }

            wedding.MyFavoritesIds.Remove(providerId);

            await _UoW.WeddingRepository.Update(wedding);
        }
    }
}