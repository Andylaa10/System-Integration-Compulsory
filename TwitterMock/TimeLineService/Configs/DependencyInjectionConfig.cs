using Microsoft.EntityFrameworkCore;
using TimeLineService.Core.Helper;
using TimeLineService.Core.Repositories;
using TimeLineService.Core.Repositories.Interfaces;
using TimeLineService.Core.Services.Interfaces;

namespace TimeLineService.Configs;

public static class DependencyInjectionConfig
{
    public static void ConfigureDi(this IServiceCollection services)
    {
        services.AddDbContext<DatabaseContext>(options =>
            options.UseInMemoryDatabase("TimeLineDb"));
        services.AddScoped<ITimeLineRepository, TimeLineRepository>();
        services.AddScoped<ITimeLineService, Core.Services.TimeLineService>();

        services.AddSingleton(AutoMapperConfig.ConfigureAutoMapper());
    }
}