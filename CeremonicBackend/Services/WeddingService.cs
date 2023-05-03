using Microsoft.AspNetCore.Http;

using System.Threading.Tasks;
using System;

using CeremonicBackend.DB.Mongo;
using CeremonicBackend.DB.Relational;
using CeremonicBackend.Services.Interfaces;
using CeremonicBackend.Repositories.Interfaces;
using CeremonicBackend.WebApiModels;
using CeremonicBackend.Exceptions;
using CeremonicBackend.Mappings;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace CeremonicBackend.Services
{
    public class WeddingService : IWeddingService
    {
        protected IUnitOfWork _UoW { get; set; }
        public WeddingService(IUnitOfWork uow)
        {
            _UoW = uow;
        }
        public void SetFileRepository(ControllerBase controller, IWebHostEnvironment env)
        {
            _UoW.SetFileRepository(controller, env);
        }



        public async Task<WeddingEntity> CreateForUser(UserEntity user)
        {
            WeddingEntity wedding = await _UoW.WeddingRepository.Add(new WeddingEntity()
            {
                UserId = user.Id,
                User1 = new PersonEntity()
                {
                    FullName = $"{user.FirstName} {user.LastName}",
                    AvatarFileName = null,
                    Email = user.LoginInfo.Email,
                    PlusGuests = 0,
                    CategoryId = 0,
                    WillCome = true,
                    Notes = "{}",
                },
                User2 = new PersonEntity()
                {
                    FullName = $"",
                    AvatarFileName = null,
                    Email = "",
                    PlusGuests = 0,
                    CategoryId = 0,
                    WillCome = true,
                    Notes = "{}",
                },
                Geolocation = null,
                Date = new DateTime(),
                GuestCountRange = new RangeEntity()
                {
                    Min = 0,
                    Max = 0,
                },
                WeddingPlan = null,
                WeddingTeam = new List<TeamRoleEntity>() { },
                MyFavorites = new List<ProviderEntity>() { },
                ApproximateBudget = new RangeEntity()
                {
                    Min = 0,
                    Max = 0,
                },
                Budget = null,
            });

            return wedding;
        }

        public async Task<WeddingApiModel> Get(int userId)
        {
            WeddingEntity wedding = await _UoW.WeddingRepository.GetById(userId);

            if (wedding is null)
            {
                throw new NotFoundAppException($"wedding not found");
            }

            return wedding.ToWeddingApiModel();
        }

        public async Task<WeddingApiModel> Get(string email)
        {
            WeddingEntity wedding = await _UoW.WeddingRepository.GetByEmail(email);

            if (wedding is null)
            {
                throw new NotFoundAppException($"wedding not found");
            }

            return wedding.ToWeddingApiModel();
        }

        public async Task<WeddingApiModel> Edit(string email, EditWeddingApiModel model)
        {
            WeddingEntity wedding = await _UoW.WeddingRepository.GetByEmail(email);

            if (wedding is null)
            {
                throw new NotFoundAppException($"wedding not found");
            }

            wedding.User1.FullName = model.FullName1 ?? "";
            wedding.User2.FullName = model.FullName2 ?? "";

            wedding.Geolocation = model.Geolocation;
            wedding.City = model.City;
            wedding.Date = model.Date;
            wedding.GuestCountRange = model.GuestCountRange;
            wedding.ApproximateBudget = model.ApproximateBudget;

            await _UoW.WeddingRepository.Update(wedding);

            return wedding.ToWeddingApiModel();
        }

        public async Task<WeddingApiModel> EditAvatar(string email, bool isMyAvatar, IFormFile avatarFile)
        {
            WeddingEntity wedding = await _UoW.WeddingRepository.GetByEmail(email);

            if (wedding is null)
            {
                throw new NotFoundAppException($"wedding not found");
            }

            string filename = await _UoW.FileRepository.Add("Avatars", avatarFile);

            string currentAvatarFileName =
                isMyAvatar ?
                wedding.User1.AvatarFileName :
                wedding.User2.AvatarFileName;

            if (!string.IsNullOrEmpty(currentAvatarFileName))
            {
                await _UoW.FileRepository.Delete("Avatars", currentAvatarFileName);
            }

            if (isMyAvatar)
            {
                wedding.User1.AvatarFileName = filename;
            }
            else
            {
                wedding.User2.AvatarFileName = filename;
            }

            await _UoW.WeddingRepository.Update(wedding);

            return wedding.ToWeddingApiModel();
        }
    }
}
