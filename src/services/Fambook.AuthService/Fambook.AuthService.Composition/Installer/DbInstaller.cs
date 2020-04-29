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
            var debug = "Data Source=fdemirci.nl;Initial Catalog=AuthDb;User ID=SA;Password=Demirci543532!";

            services.AddTransient<ApplicationDbContext>()
                .AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(
                    debug ?? throw new InvalidOperationException()));

        }
    }
}
