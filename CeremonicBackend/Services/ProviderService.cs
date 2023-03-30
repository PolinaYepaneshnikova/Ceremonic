using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using System;
using System.Threading.Tasks;

using CeremonicBackend.DB.Mongo;
using CeremonicBackend.DB.Relational;
using CeremonicBackend.Exceptions;
using CeremonicBackend.Repositories.Interfaces;
using CeremonicBackend.Services.Interfaces;
using CeremonicBackend.WebApiModels;
using CeremonicBackend.Mappings;

namespace CeremonicBackend.Services
{
    public class ProviderService : IProviderService
    {
        protected IUnitOfWork _UoW { get; set; }
        public ProviderService(IUnitOfWork uow)
        {
            _UoW = uow;
        }
        public void SetFileRepository(ControllerBase controller, IWebHostEnvironment env)
        {
            _UoW.SetFileRepository(controller, env);
        }



        public async Task<ProviderApiModel> CreateForUser(UserEntity user, ProviderInfoApiModel providerInfo)
        {
            ProviderEntity provider = await _UoW.ProviderRepository.Add(new ProviderEntity()
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

            return await provider.ToProviderApiModel(_UoW.ServiceRepository);
        }

        public async Task<ProviderApiModel> Get(int userId)
        {
            ProviderEntity provider = await _UoW.ProviderRepository.GetById(userId);

            if (provider is null)
            {
                throw new NotFoundAppException($"provider not found");
            }

            return await provider.ToProviderApiModel(_UoW.ServiceRepository);
        }

        public async Task<ProviderApiModel> Get(string email)
        {
            ProviderEntity provider = await _UoW.ProviderRepository.GetByEmail(email);

            if (provider is null)
            {
                throw new NotFoundAppException($"provider not found");
            }

            return await provider.ToProviderApiModel(_UoW.ServiceRepository);
        }

        public async Task<ProviderApiModel> Edit(string email, ProviderEditApiModel model)
        {
            ProviderEntity provider = await _UoW.ProviderRepository.GetByEmail(email);

            if (provider is null)
            {
                throw new NotFoundAppException($"provider not found");
            }

            provider.Info = model.Info;
            provider.Geolocation = model.Geolocation;
            provider.City = model.City;
            provider.AveragePrice = model.AveragePrice;

            if (model.AddedImageFiles is not null)
            {
                foreach (IFormFile imageFile in model.AddedImageFiles)
                {
                    if (imageFile is not null)
                    {
                        provider.ImageFileNames.Add(await _UoW.FileRepository.Add("Images", imageFile));
                    }
                }
            }

            if (model.DeletedImageNames is not null)
            {
                foreach (string imageName in model.DeletedImageNames)
                {
                    if (!string.IsNullOrEmpty(imageName))
                    {
                        await _UoW.FileRepository.Delete("Images", imageName);
                    }
                    provider.ImageFileNames.Remove(imageName);
                }
            }

            await _UoW.ProviderRepository.Update(provider);

            return await provider.ToProviderApiModel(_UoW.ServiceRepository);
        }

        public async Task<ProviderApiModel> EditAvatar(string email, IFormFile avatarFile)
        {
            ProviderEntity provider = await _UoW.ProviderRepository.GetByEmail(email);

            if (provider is null)
            {
                throw new NotFoundAppException($"provider not found");
            }

            string filename = await _UoW.FileRepository.Add("Avatars", avatarFile);
            if (!string.IsNullOrEmpty(provider.AvatarFileName))
            {
                await _UoW.FileRepository.Delete("Avatars", provider.AvatarFileName);
            }
            provider.AvatarFileName = filename;

            await _UoW.ProviderRepository.Update(provider);

            return await provider.ToProviderApiModel(_UoW.ServiceRepository);
        }
    }
}
