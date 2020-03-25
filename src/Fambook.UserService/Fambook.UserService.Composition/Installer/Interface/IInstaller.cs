using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Fambook.UserService.Composition.Installer.Interface
{
    public interface IInstaller
    {
        void InstallServices(IServiceCollection services, IConfiguration configuration);
    }
}
