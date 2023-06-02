using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace CeremonicBackend.DB.Relational
{
    public class CeremonicRelationalDbContext : DbContext
    {
        public CeremonicRelationalDbContext(DbContextOptions<CeremonicRelationalDbContext> options) : base(options) { }

        public DbSet<UserEntity> Users { get; set; }
        public DbSet<UserLoginInfoEntity> UserLoginInfos { get; set; }
        public DbSet<ServiceEntity> Services { get; set; }
        public DbSet<AgreementEntity> Agreements { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ScalarReturn<int>>().HasNoKey().ToView(null);

            builder.Entity<ServiceEntity>().HasData(new ServiceEntity[]
            {
                new ServiceEntity()
                {
                    Id = 1,
                    Name = "Банкетна зала",
                    UnitOfService = "*за годину аренди",
                },
                new ServiceEntity()
                {
                    Id = 2,
                    Name = "Місце проведення церемонії",
                    UnitOfService = "*за годину аренди",
                },
                new ServiceEntity()
                {
                    Id = 3,
                    Name = "Цермоніймейстр",
                    UnitOfService = "*за церемонію",
                },
                new ServiceEntity()
                {
                    Id = 4,
                    Name = "Ведучий",
                    UnitOfService = "*за весілля",
                },
                new ServiceEntity()
                {
                    Id = 5,
                    Name = "Їжа/кайтеринг",
                    UnitOfService = "*середній чек на одну людину",
                },
                new ServiceEntity()
                {
                    Id = 6,
                    Name = "Флористика та декор",
                    UnitOfService = "*на весілля",
                },
                new ServiceEntity()
                {
                    Id = 7,
                    Name = "Декор та освіщення",
                    UnitOfService = "*за стандартну композицію",
                },
                new ServiceEntity()
                {
                    Id = 8,
                    Name = "Поліграфія",
                    UnitOfService = "*усі паперові вироби для весілля",
                },
                new ServiceEntity()
                {
                    Id = 9,
                    Name = "Кондитер",
                    UnitOfService = "*за кг весільного торту",
                },
                new ServiceEntity()
                {
                    Id = 10,
                    Name = "Фотозйомка",
                    UnitOfService = "*за весілля",
                },
                new ServiceEntity()
                {
                    Id = 11,
                    Name = "Відеозйомка",
                    UnitOfService = "*за весілля",
                },
                new ServiceEntity()
                {
                    Id = 12,
                    Name = "Музика",
                    UnitOfService = "*за годину роботи",
                },
                new ServiceEntity()
                {
                    Id = 13,
                    Name = "Технічна підтримка",
                    UnitOfService = "*за годину роботи",
                },
                new ServiceEntity()
                {
                    Id = 14,
                    Name = "Візажист",
                    UnitOfService = "*за весільний макіяж",
                },
                new ServiceEntity()
                {
                    Id = 15,
                    Name = "Перукар",
                    UnitOfService = "*за весільну укладку",
                },
                new ServiceEntity()
                {
                    Id = 16,
                    Name = "Автомобіль наречених",
                    UnitOfService = "*за годину аренди",
                },
                new ServiceEntity()
                {
                    Id = 17,
                    Name = "Транспорт для гостей",
                    UnitOfService = "*за кожні 10 км",
                },
                new ServiceEntity()
                {
                    Id = 18,
                    Name = "Букет нареченої",
                    UnitOfService = "*за букет",
                },
            });
        }
    }
}
