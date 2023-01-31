using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using System.Threading.Tasks;

using CeremonicBackend.DB.Relational;
using System.Linq;

namespace CeremonicBackend
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var app = CreateHostBuilder(args).Build();
            await MigrateDatabaseAsync(app);
            app.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

        public static async Task MigrateDatabaseAsync(IHost host)
        {
            using var scope = host.Services.CreateScope();

            var relationalDB = scope.ServiceProvider.GetRequiredService<CeremonicRelationalDbContext>();
            await relationalDB.Database.MigrateAsync();

            if (relationalDB.Users.ToArray().Length == 0)
            {
                relationalDB.Users.Add(new UserEntity()
                {
                    FirstName = "Ганна",
                    LastName = "Павлюк",
                });
                relationalDB.SaveChanges();
            }

            if (relationalDB.UserLoginInfos.ToArray().Length == 0)
            {
                relationalDB.UserLoginInfos.Add(new UserLoginInfoEntity()
                {
                    Email = "hanna.pavliuk@nure.ua",
                    PasswordHash = "A6xnQhbz4Vx2HuGl4lXwZ5U2I8iziLRFnhP5eNfIRvQ=",
                });
                relationalDB.SaveChanges();
            }
        }
    }
}