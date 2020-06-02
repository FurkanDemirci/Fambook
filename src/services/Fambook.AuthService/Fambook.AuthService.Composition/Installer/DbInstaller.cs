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
            var debug = "Server=tcp:fambook.database.windows.net,1433;Initial Catalog=AuthDatabase;Persist Security Info=False;User ID=FurkanDemirci;Password=Demirci543532;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

            services.AddTransient<ApplicationDbContext>()
                .AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(
                    Environment.GetEnvironmentVariable("FAMBOOK_AUTHSERVICE_DB") ?? throw new InvalidOperationException()));

        }
    }
}
