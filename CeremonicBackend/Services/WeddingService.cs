using System.Threading.Tasks;
using System;

using CeremonicBackend.DB.Mongo;
using CeremonicBackend.DB.Relational;
using CeremonicBackend.Services.Interfaces;
using CeremonicBackend.Repositories.Interfaces;

namespace CeremonicBackend.Services
{
    public class WeddingService : IWeddingService
    {
        protected IUnitOfWork _UoW { get; set; }
        public WeddingService(IUnitOfWork uow)
        {
            _UoW = uow;
        }



        public async Task<WeddingEntity> CreateForUser(UserEntity user)
        {
            return await _UoW.WeddingRepository.Add(new WeddingEntity()
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
                WeddingTeam = new object[] { },
                ApproximateBudget = new RangeEntity()
                {
                    Min = 0,
                    Max = 0,
                },
                Budget = null,
            });
        }
    }
}
