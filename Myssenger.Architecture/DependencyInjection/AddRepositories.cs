using Microsoft.Extensions.DependencyInjection;
using Mysennger.Domain;
using Mysennger.Domain.Posts;
using Mysennger.Domain.Subriddots;
using Mysennger.Domain.Users;

namespace Mysennger.Architecture.DependencyInjection;

public static class AddRepositories
{
    public static IServiceCollection RegisterRepositories(this IServiceCollection collection)
    {
        collection.AddScoped<IUnitOfWork, UnitOfWork>();
        return collection;
    } 
}