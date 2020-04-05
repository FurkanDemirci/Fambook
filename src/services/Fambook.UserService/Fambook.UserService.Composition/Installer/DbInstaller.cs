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
            var debug = "Data Source=fdemirci.nl;Initial Catalog=UserDb;User ID=SA;Password=Demirci543532!";

            services.AddTransient<ApplicationDbContext>()
                .AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(
                    Environment.GetEnvironmentVariable("FAMBOOK_USERSERVICE_DB") ?? throw new InvalidOperationException()));
        }
    }
}
