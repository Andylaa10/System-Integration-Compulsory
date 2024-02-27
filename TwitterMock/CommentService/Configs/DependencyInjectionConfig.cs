using CommentService.Core.Helper;
using CommentService.Core.Repositories;
using CommentService.Core.Repositories.Interfaces;
using CommentService.Core.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CommentService.Configs;

public static class DependencyInjectionConfig
{
    public static void ConfigureDi(this IServiceCollection services)
    {
        services.AddDbContext<DatabaseContext>(options =>
            options.UseInMemoryDatabase("CommentDb"));
        services.AddScoped<ICommentRepository, CommentRepository>();
        services.AddScoped<ICommentService, Core.Services.CommentService>();
        services.AddSingleton(AutoMapperConfig.ConfigureAutoMapper());
    }
}