using Fambook.UserService.Composition.Installer.Interface;
using Fambook.UserService.Services;
using Fambook.UserService.Services.Helpers;
using Fambook.UserService.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.ObjectPool;
using RabbitMQ.Client;

namespace Fambook.UserService.Composition.Installer
{
    public class RabbitInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<ObjectPoolProvider, DefaultObjectPoolProvider>();
            services.AddSingleton<IPooledObjectPolicy<IModel>, RabbitModelPooledObjectPolicy>();
            services.AddSingleton<IRabbitManager, RabbitManager>();
        }
    }
}
