using Microsoft.Extensions.DependencyInjection;
using Mysennger.Domain;

namespace Mysennger.Architecture.DependencyInjection;

public static class AddRepositories
{
    public static IServiceCollection RegisterRepositories(this IServiceCollection collection)
    {
        return collection;
    } 
}