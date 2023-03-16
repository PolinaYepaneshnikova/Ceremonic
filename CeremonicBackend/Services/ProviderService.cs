using System;
using System.Threading.Tasks;

using CeremonicBackend.DB.Mongo;
using CeremonicBackend.DB.Relational;
using CeremonicBackend.Exceptions;
using CeremonicBackend.Repositories.Interfaces;
using CeremonicBackend.Services.Interfaces;
using CeremonicBackend.WebApiModels;

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

        public async Task<ProviderEntity> Edit(EditProviderApiModel model)
        {
            ProviderEntity provider = await _UoW.ProviderRepository.GetById(model.UserId);

            if (provider is null)
            {
                throw new NotFoundAppException($"provider not found");
            }

            return await _UoW.ProviderRepository.Update(new ProviderEntity()
            {
                UserId = model.UserId,
                ServiceId = provider.ServiceId,
                BrandName = provider.BrandName,
                AvatarFileName = provider.AvatarFileName,
                ImageFileNames = provider.ImageFileNames,
                Info = model.Info,
                PlaceName = provider.PlaceName,
                Geolocation = model.Geolocation,
                City = model.City,
                AveragePrice = model.AveragePrice,
                WorkingDayList = provider.WorkingDayList,
            });
        }
    }
}
