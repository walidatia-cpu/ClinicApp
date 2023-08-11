using ClinicApp.BLL.Services;
using ClinicApp.BLL.Services.Identity;
using ClinicApp.Core.Contracts;
using ClinicApp.Core.Contracts.Identity;
using ClinicApp.Core.Entities;
using ClinicApp.Core.JWT;
using ClinicApp.DAL.Data;
using ClinicApp.Security;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.ProjectModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using NuGet.Protocol.Core.Types;
using NuGet.Protocol.Plugins;
using System.Configuration;
using System.Text;

namespace ClinicApp.Extensions
{
    public static class MyConfigServiceCollectionExtensions
    {
        public static IServiceCollection AddConfig(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(config.GetConnectionString("DefaultConnection")));
            services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
            {
                // Configure identity options if needed
                options.SignIn.RequireConfirmedAccount = true;
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
            }).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });
            return services;
        }
        public static IServiceCollection AddMyDependencyGroup(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(IAsyncRepository<>), typeof(Repository<>));
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<JwtAuthorizeAttribute>();

            return services;
        }
        public static IServiceScope MigrateDatabase(this IServiceScope app)
        {
            #region applay migration
            var dataContext = app.ServiceProvider.GetRequiredService<AppDbContext>();
            dataContext.Database.Migrate();
            #endregion
            return app;
        }
        public async static Task<IServiceScope> SeedDefaultData(this IServiceScope app)
        {
            #region Seed Default Data
            var roleService = app.ServiceProvider.GetRequiredService<IRoleService>();
            var userService = app.ServiceProvider.GetRequiredService<IUserService>();
            await roleService.CreateDefaultRolesAsync();
            await userService.CreateDefaultUsersAsync();
            #endregion
            return app;
        }
        public static IServiceCollection AddJWTAuthentication(this IServiceCollection services, IConfiguration config)
        {
            var appSettingsSection = config.GetSection("JwtSettings");
            services.Configure<JWTSettings>(appSettingsSection);
            
            var jwtSettings = config.GetSection("JwtSettings").Get<JWTSettings>();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                        .AddJwtBearer(options =>
                        {
        
                            options.TokenValidationParameters = new TokenValidationParameters
                            {
                                ValidateIssuerSigningKey = true,
                                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSettings.Secret)),
                                ValidateIssuer = true,
                                ValidateAudience = true,
                                ValidIssuer = jwtSettings.Issuer,
                                ValidAudience = jwtSettings.Audience
                            };
                           
                        });
            return services;
        }
    }
}
