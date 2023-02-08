using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using CeremonicBackend.DB.Mongo;
using CeremonicBackend.DB.Relational;
using MongoDB.Bson;
using MongoDB.Driver;
using CeremonicBackend.Authentification;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using CeremonicBackend.Repositories.Interfaces;
using CeremonicBackend.Repositories;
using CeremonicBackend.Services.Interfaces;
using CeremonicBackend.Services;

namespace CeremonicBackend
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            Configuration.Bind("AuthOptions", new AuthOptions());

            services.AddDbContext<CeremonicRelationalDbContext>(options =>
                options.UseSqlite(Configuration.GetConnectionString("SqliteConnection")));

            var relationalDB = services.BuildServiceProvider()
                .GetRequiredService<CeremonicRelationalDbContext>();

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

            services.AddScoped<ICeremonicMongoDbContext, CeremonicMongoDbContext>(
                provider =>
                    new CeremonicMongoDbContext(
                        provider.GetRequiredService<IConfiguration>().GetConnectionString("MongoDbConnection"),
                        provider.GetRequiredService<IConfiguration>().GetConnectionString("MongoDbName")
                    )
            );

            var mongoDB = services.BuildServiceProvider()
                .GetRequiredService<ICeremonicMongoDbContext>();

            mongoDB.CreateIndexes();

            /*if (mongoDB.Weddings.Find(new BsonDocument()).ToList().Count == 0)
            {
                mongoDB.Weddings.InsertOne(new WeddingEntity()
                {
                    UserId = 1,
                    Wife = new PersonEntity()
                    {
                        Id = Guid.NewGuid(),
                        FullName = "Ганна Павлюк",
                        AvatarFileName = null,
                        Email = "hanna.pavliuk@nure.ua",
                        PlusGuests = 0,
                        CategoryId = 0,
                        WillCome = true,
                    },
                    Husband = new PersonEntity()
                    {
                        Id = Guid.NewGuid(),
                        FullName = "Павло Перебийніс",
                        AvatarFileName = null,
                        Email = "pavlo.perebyinis@nure.ua",
                        PlusGuests = 0,
                        CategoryId = 0,
                        WillCome = true,
                    },
                    Geolocation = "@50.401699,30.252512",
                    Date = new DateTime(2023, 11, 15),
                    GuestCountRange = new RangeEntity()
                    {
                        Min = 70,
                        Max = 100,
                    },
                    GuestMap = null,
                    WeddingPlan = null,
                    WeddingTeam = { },
                    ApproximateBudget = new RangeEntity()
                    {
                        Min = 70000,
                        Max = 100000,
                    },
                    Budget = null,
                });
            }*/



            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "CeremonicBackend", Version = "v1" });
            });



            services.AddCors(options =>
            {
                options.AddPolicy("AllowAllHeaders",
                    builder =>
                    {
                        builder.AllowAnyOrigin()
                               .AllowAnyHeader()
                               .AllowAnyMethod();
                    });
            });



            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IUserService, UserService>();

            services.AddScoped<IUserRepository, UserRepository>();



            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = AuthOptions.Issuer,
                        ValidateAudience = true,
                        ValidAudience = AuthOptions.Audience,
                        ValidateLifetime = true,
                        IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
                        ValidateIssuerSigningKey = true
                    };

                    options.Events = new JwtBearerEvents
                    {
                        OnMessageReceived = context =>
                        {
                            var accessToken = context.Request.Query["access_token"];

                            // If the request is for our hub...
                            var path = context.HttpContext.Request.Path;
                            if (!string.IsNullOrEmpty(accessToken) &&
                                (path.StartsWithSegments("/messenger") || path.StartsWithSegments("/IoT")))
                            {
                                // Read the token out of the query string
                                context.Token = accessToken;
                            }
                            return Task.CompletedTask;
                        }
                    };
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CeremonicBackend v1"));
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseCors("AllowAllHeaders");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
