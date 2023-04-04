using System.ComponentModel.Design;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Mysennger.Architecture.DependencyInjection;

public static class AddDbContext
{
    public static IServiceCollection RegisterDbContext(this IServiceCollection container, IConfiguration configuration)
    {
        container.AddDbContext<MsgContext>(options =>
        {
            var hostName = configuration["Myssenger:DbHost"]!;
            var userName = configuration["Myssenger:DbUser"]!;
            var userPassword = configuration["Myssenger:DbPassword"]!;
            var dbName = configuration["Myssenger:DbName"]!;

            options.UseNpgsql(
                $"User ID={userName}; Password={userPassword}; Host={hostName}; Port={5432}; Database={dbName}");
        });

        return container;
    }
}