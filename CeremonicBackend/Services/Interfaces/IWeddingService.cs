using System.Threading.Tasks;

using CeremonicBackend.DB.Mongo;
using CeremonicBackend.DB.Relational;
using CeremonicBackend.WebApiModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CeremonicBackend.Services.Interfaces
{
    public interface IWeddingService
    {
        void SetFileRepository(ControllerBase controller, IWebHostEnvironment env);

        Task<WeddingEntity> CreateForUser(UserEntity user);
        Task<WeddingApiModel> Get(int userId);
        Task<WeddingApiModel> Get(string email);
        Task<WeddingApiModel> Edit(string email, EditWeddingApiModel model);
        Task<WeddingApiModel> EditAvatar(string email, bool isMyAvatar, IFormFile avatarFile);
    }
}
