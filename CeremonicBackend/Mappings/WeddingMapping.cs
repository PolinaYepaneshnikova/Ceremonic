using CeremonicBackend.DB.Mongo;
using CeremonicBackend.Repositories.Interfaces;
using CeremonicBackend.WebApiModels;
using System.Linq;
using System.Threading.Tasks;

namespace CeremonicBackend.Mappings
{
    public static class WeddingMapping
    {
        public static WeddingApiModel ToWeddingApiModel(this WeddingEntity entity)
            => new WeddingApiModel()
            {
                UserId = entity.UserId,

                FullName1 = entity.User1.FullName,
                Avatar1FileName = entity.User1.AvatarFileName,
                FullName2 = entity.User2.FullName,
                Avatar2FileName = entity.User2.AvatarFileName,

                Date = entity.Date,
                Geolocation = entity.Geolocation,
                City = entity.City,

                GuestCountRange = entity.GuestCountRange,
                ApproximateBudget = entity.ApproximateBudget,
            };

        public static async Task<WeddingEntity> IncludeFavorites(this WeddingEntity entity, IProviderRepository providerRepository)
        {
            await Task.Run(() => { });

            var myFavEnumerable = entity.MyFavoritesIds
                .Select(async id => await providerRepository.GetById(id))
                .Select(task => task.Result);

            entity.MyFavorites = myFavEnumerable.ToList();

            return entity;
        }
    }
}
