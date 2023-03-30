using System.Threading.Tasks;

using CeremonicBackend.DB.Mongo;
using CeremonicBackend.Repositories.Interfaces;
using CeremonicBackend.WebApiModels;

namespace CeremonicBackend.Mappings
{
    public static class ProviderMapping
    {
        public static async Task<ProviderApiModel> ToProviderApiModel(this ProviderEntity entity, IServiceRepository serviceRepository)
            => new ProviderApiModel()
            {
                UserId = entity.UserId,
                ServiceName = (await serviceRepository.GetById(entity.ServiceId)).RelationalServiceEntity.Name,
                BrandName = entity.BrandName,
                AvatarFileName = entity.AvatarFileName,
                ImageFileNames = entity.ImageFileNames,
                Info = entity.Info,
                PlaceName = entity.PlaceName,
                Geolocation = entity.Geolocation,
                AveragePrice = entity.AveragePrice,
                WorkingDayList = entity.WorkingDayList,
            };
    }
}
