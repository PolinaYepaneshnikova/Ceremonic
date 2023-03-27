using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

using System.Threading.Tasks;

using CeremonicBackend.DB.Mongo;
using CeremonicBackend.DB.Relational;
using CeremonicBackend.WebApiModels;
using Microsoft.AspNetCore.Http;

namespace CeremonicBackend.Services.Interfaces
{
    public interface IProviderService
    {
        void SetFileRepository(ControllerBase controller, IWebHostEnvironment env);

        Task<ProviderEntity> CreateForUser(UserEntity user, ProviderInfoApiModel providerInfo);
        Task<ProviderEntity> Edit(string email, ProviderEditApiModel model);
        Task<ProviderEntity> EditAvatar(string email, IFormFile avatarFile);
    }
}
