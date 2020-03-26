using System;
using Fambook.UserService.Composition.Installer.Interface;
using Fambook.UserService.DataAccess.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Fambook.UserService.Composition.Installer
{
    public class DbInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<ApplicationDbContext>()
                .AddEntityFrameworkSqlServer()
                .AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(
                    Environment.GetEnvironmentVariable("FAMBOOK_USERSERVICE_DB") ?? throw new InvalidOperationException()));
        }
    }
}
