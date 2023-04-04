using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Mysennger.Architecture.DependencyInjection;

public static class AddMediator
{
    public static IServiceCollection RegisterMediator(this IServiceCollection collection)
    {
        collection.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
        });

        return collection;
    }
}