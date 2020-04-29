using System;
using System.Collections.Generic;
using System.Text;
using Fambook.AuthService.Composition.Installer.Interface;
using Fambook.AuthService.Services.Helpers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Fambook.AuthService.Composition.Installer
{
    public class HostedServiceInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddHostedService<ConsumeRabbitMqHostedService>();
        }
    }
}
