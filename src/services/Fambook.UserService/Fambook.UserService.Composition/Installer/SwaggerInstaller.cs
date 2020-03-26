using System;
using System.Collections.Generic;
using System.Text;
using Fambook.UserService.Composition.Installer.Interface;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Fambook.UserService.Composition.Installer
{
    public class SwaggerInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddSwaggerGen(x =>
            {
                x.SwaggerDoc("Fambook", new OpenApiInfo { Title = "Fambook User Service", Version = "v1" });
            });
        }
    }
}
