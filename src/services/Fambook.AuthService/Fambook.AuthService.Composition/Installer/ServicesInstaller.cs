using Fambook.AuthService.Composition.Installer.Interface;
using Fambook.AuthService.DataAccess.Data.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Fambook.AuthService.Composition.Installer
{
    public class ServicesInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IAuthService, DataAccess.Data.Services.AuthService>();
        }
    }
}
