using System.Threading.Tasks;

using CeremonicBackend.DB.Mongo;
using CeremonicBackend.Repositories.Interfaces;
using CeremonicBackend.WebApiModels;

namespace CeremonicBackend.Mappings
{
    public static class ProviderMapping
    {
        public static async Task<ProviderApiModel> ToProviderApiModel(this ProviderEntity entity, IServiceRepository serviceRepository)
        {
            string serviceName = (await serviceRepository.GetById(entity.ServiceId)).RelationalServiceEntity.Name;

            if (entity is PlaceProviderEntity placeEntity)
            {
                return new PlaceProviderApiModel()
                {
                    UserId = entity.UserId,
                    ServiceName = serviceName,
                    BrandName = entity.BrandName,
                    AvatarFileName = entity.AvatarFileName,
                    ImageFileNames = entity.ImageFileNames,
                    Info = entity.Info,
                    PlaceName = entity.PlaceName,
                    Geolocation = entity.Geolocation,
                    City = entity.City,
                    AveragePrice = entity.AveragePrice,
                    GuestCount = placeEntity.GuestCount,
                    WorkingDayList = entity.WorkingDayList,
                };
            }

            return new ProviderApiModel()
            {
                UserId = entity.UserId,
                ServiceName = serviceName,
                BrandName = entity.BrandName,
                AvatarFileName = entity.AvatarFileName,
                ImageFileNames = entity.ImageFileNames,
                Info = entity.Info,
                PlaceName = entity.PlaceName,
                Geolocation = entity.Geolocation,
                City = entity.City,
                AveragePrice = entity.AveragePrice,
                WorkingDayList = entity.WorkingDayList,
            };
        }
    }
}
