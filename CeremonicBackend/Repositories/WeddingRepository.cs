using System;
using System.Threading.Tasks;

using CeremonicBackend.DB.Mongo;
using CeremonicBackend.Repositories.Interfaces;
using CeremonicBackend.DB.Relational;

namespace CeremonicBackend.Repositories
{
    public class WeddingRepository : BaseMongoRepositoryWithUserIdKey<WeddingEntity>, IWeddingRepository
    {
        public const string ConstCollectionName = "weddings";

        public WeddingRepository(ICeremonicMongoDbContext db) : base(db, ConstCollectionName) { }

        public async Task<WeddingEntity> CreateForUser(UserEntity user)
        {
            return await Add(new WeddingEntity()
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
                GuestCountRange = null,
                WeddingPlan = null,
                WeddingTeam = new object[] { },
                ApproximateBudget = null,
                Budget = null,
            });
        }
    }
}
