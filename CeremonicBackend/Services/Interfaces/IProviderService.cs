using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using System.Threading.Tasks;
using System.Collections.Generic;

using CeremonicBackend.DB.Relational;
using CeremonicBackend.WebApiModels;

namespace CeremonicBackend.Services.Interfaces
{
    public interface IProviderService
    {
        void SetFileRepository(ControllerBase controller, IWebHostEnvironment env);

        Task<ProviderApiModel> CreateForUser(UserEntity user, ProviderInfoApiModel providerInfo);
        Task<ProviderApiModel> Get(int userId);
        Task<ProviderApiModel> Get(string email);
        Task<ProviderApiModel> Edit(string email, EditProviderApiModel model);
        Task<ProviderApiModel> EditAvatar(string email, IFormFile avatarFile);
        Task<List<ProviderApiModel>> Search(SearchProviderApiModel model);
    }
}
