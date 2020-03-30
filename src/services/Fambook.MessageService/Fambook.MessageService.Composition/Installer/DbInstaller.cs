using System;
using Fambook.MessageService.Composition.Installer.Interface;
using Fambook.MessageService.DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Fambook.MessageService.Composition.Installer
{
    class DbInstaller : IInstaller
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
