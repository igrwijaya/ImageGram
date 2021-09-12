using System;
using ImageGram.Core.Application.Services;
using ImageGram.Core.Constant.Constant;
using ImageGram.Core.Domain.AggregateRoots.Account;
using ImageGram.Core.Domain.AggregateRoots.Post;
using ImageGram.Core.Infrastructure.DataSources;
using ImageGram.Core.Infrastructure.Models;
using ImageGram.Core.Infrastructure.Repositories.Common;
using ImageGram.Core.Infrastructure.Repositories.Post;
using ImageGram.Core.Infrastructure.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ImageGram.Core.Infrastructure
{
    public static class CoreInfrastructureStartup
    {
        public static void AddCoreInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<CoreDbContext>(    
                dbContextOptions => dbContextOptions
                    .UseMySql(
                        configuration[ConfigurationConstant.ConnMysql],
                        new MySqlServerVersion(new Version(8, 0)))
            );
            
            services.AddIdentity<AppUserIdentity, IdentityRole>(options => 
                {
                    options.Password.RequireDigit = false;
                    options.Password.RequiredLength = 6;
                    options.Password.RequireLowercase = true;
                    options.Password.RequireUppercase = false;
                    options.Password.RequireNonAlphanumeric = false;
                    options.User.RequireUniqueEmail = true;
                })
                .AddEntityFrameworkStores<CoreDbContext>()
                .AddDefaultTokenProviders();

            #region Service

            services.AddScoped<IIdentityService, IdentityService>();
            services.AddScoped<IStorageService, StorageService>();

            #endregion

            #region Repositories

            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<IPostRepository, PostRepository>();

            #endregion

        }
    }
}