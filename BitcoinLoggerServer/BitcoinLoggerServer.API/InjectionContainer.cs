using BitcoinLoggerServer.Contracts.Models;
using BitcoinLoggerServer.Framework.Interfaces;
using BitcoinLoggerServer.Framework.Security;
using BitcoinLoggerServer.Repositories;
using BitcoinLoggerServer.Repositories.DBContext;
using BitcoinLoggerServer.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;

namespace BitcoinLoggerServer.API
{
    public static class InjectionContainer
    {
        public static void InjectEntities(IServiceCollection services, IConfiguration configuration)
        {
            //DEPENDENCIES
            //Transient – Created every time they are requested
            //Scoped – Created once per scope. Most of the time, scope refers to a web request.But this can also be used for any unit of work, such as the execution of an Azure Function.
            //Singleton – Created only for the first request.If a particular instance is specified at registration time, this instance will be provided to all consumers of the registration type.

            //SAMPLES OF GENERIC INJECTION
            //services.AddScoped<IBaseService<T>, BaseService<T>>();
            //services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
                        
            string connString = configuration.GetConnectionString("dbConnection");
            //BitcoinLoggerContext dbContext = new BitcoinLoggerContext(connString);
            services.AddScoped<DbContext, DbContext>(context => new BitcoinLoggerContext(connString));


            //services.AddScoped<UserRepository, UserRepository>(repo => new UserRepository(dbContext));
            services.AddScoped<UserRepository, UserRepository>();
            services.AddScoped<UserService, UserService>();
            services.AddScoped<User, User>();

            //services.AddScoped<UserSessionRepository, UserSessionRepository>(repo => new UserSessionRepository(dbContext));
            services.AddScoped<UserSessionRepository, UserSessionRepository>();
            services.AddScoped<UserSessionService, UserSessionService>();
            services.AddScoped<UserSession, UserSession>();

            //services.AddScoped<SourceRepository, SourceRepository>(repo => new SourceRepository(dbContext));
            services.AddScoped<SourceRepository, SourceRepository>();
            services.AddScoped<SourceService, SourceService>();
            services.AddScoped<Source, Source>();

            services.AddScoped<AuthenticationService, AuthenticationService>();
                        
            //IConfigurationSection appSettingsSection = configuration.GetSection("Endpoints");
            //List<string> endpoints = appSettingsSection.Get<List<string>>();

            services.AddScoped<HttpClient, HttpClient>();
            services.AddScoped<CurrencyPairService, CurrencyPairService>();
            

        }

        public static void AddAuthentication(IServiceCollection services, IConfiguration configuration)
        {

            //GET APP SETTINGS SECTION
            IConfigurationSection appSettingsSection = configuration.GetSection("JWTSettings");
            JWTSettings jwtSettings = appSettingsSection.Get<JWTSettings>();
                        
            services.AddSingleton<IJWTSettings, JWTSettings>(JWTSettings => jwtSettings);
            services.AddScoped<JWTService, JWTService>();
            services.AddScoped<EncryptionService, EncryptionService>();

            //ADD JWT AUTHENTICATION
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSettings.PrivateKey)),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
        }

        //public static void AddAuthorization(IServiceCollection services)
        //{
        //    //ADD AUTHORIZATION
        //    services.AddAuthorization(options =>
        //    {
        //        options.AddPolicy("Admin", policy => policy.RequireClaim("UserEmail", new List<string> { "test@hotmail.com" }));
        //    });
        //}
    }
}
