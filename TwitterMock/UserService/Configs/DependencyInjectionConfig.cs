using Microsoft.EntityFrameworkCore;
using UserService.Core.Helper;

namespace UserService.Configs;

public static class DependencyInjectionConfig
{
    public static void ConfigureDi(this IServiceCollection services)
    {
        services.AddDbContext<DatabaseContext>(options =>
        {
            options.UseInMemoryDatabase("UserDb");
        });
        services.AddSingleton(AutoMapperConfig.ConfigureAutoMapper());
    }
}