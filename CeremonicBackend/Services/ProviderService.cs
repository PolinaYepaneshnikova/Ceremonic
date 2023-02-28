using CeremonicBackend.DB.Mongo;
using CeremonicBackend.DB.Relational;
using CeremonicBackend.Repositories.Interfaces;
using CeremonicBackend.Services.Interfaces;
using CeremonicBackend.WebApiModels;
using System;
using System.Threading.Tasks;

namespace CeremonicBackend.Services
{
    public class ProviderService : IProviderService
    {
        protected IUnitOfWork _UoW { get; set; }
        public ProviderService(IUnitOfWork uow)
        {
            _UoW = uow;
        }



        public async Task<ProviderEntity> CreateForUser(UserEntity user, ProviderInfoApiModel providerInfo)
        {
            return await _UoW.ProviderRepository.Add(new ProviderEntity()
            {
                UserId= user.Id,
                ServiceId = (await _UoW.ServiceRepository.GetByName(providerInfo.ServiceName)).Id,
                BrandName = providerInfo.BrandName,
                AvatarFileName = null,
                ImageFileNames = { },
                PlaceName = "",
                Geolocation = null,
                City = "",
                AveragePrice = new RangeEntity()
                {
                    Min = 0,
                    Max = 0,
                },
                WorkingDayList = null,
            });
        }
    }
}
