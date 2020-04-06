using System;
using System.Collections.Generic;
using System.Text;
using Fambook.AuthService.Composition.Installer.Interface;
using Fambook.AuthService.DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Fambook.AuthService.Composition.Installer
{
    public class DbInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<ApplicationDbContext>()
                .AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(
                    Environment.GetEnvironmentVariable("FAMBOOK_AUTHSERVICE_DB") ?? throw new InvalidOperationException()));

        }
    }
}
