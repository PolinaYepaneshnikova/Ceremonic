using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace CeremonicBackend.DB.Relational
{
    public class CeremonicRelationalDbContext : DbContext
    {
        public CeremonicRelationalDbContext(DbContextOptions<CeremonicRelationalDbContext> options) : base(options) { }

        public DbSet<UserEntity> Users { get; set; }
        public DbSet<UserLoginInfoEntity> UserLoginInfos { get; set; }
        public DbSet<ServiceEntity> Services { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ScalarReturn<int>>().HasNoKey().ToView(null);
        }
    }
}
