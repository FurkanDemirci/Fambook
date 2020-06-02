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
            var debug = "Server=tcp:fambook.database.windows.net,1433;Initial Catalog=UserDatabase;Persist Security Info=False;User ID=FurkanDemirci;Password=Demirci543532;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

            services.AddTransient<ApplicationDbContext>()
                .AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(
                    Environment.GetEnvironmentVariable("FAMBOOK_USERSERVICE_DB") ?? throw new InvalidOperationException()));
        }
    }
}
