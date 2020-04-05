using System;
using System.Collections.Generic;
using System.Text;
using Fambook.UserService.Composition.Installer.Interface;
using Fambook.UserService.DataAccess.Data.Repository;
using Fambook.UserService.DataAccess.Data.Repository.IRepository;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Fambook.UserService.Composition.Installer
{
    public class RepoInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
    }
}
