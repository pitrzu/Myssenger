using Microsoft.Extensions.DependencyInjection;
using Mysennger.Domain;
using Mysennger.Domain.Posts;
using Mysennger.Domain.Subriddots;
using Mysennger.Domain.Users;
using Myssenger.Application.Repositories;

namespace Mysennger.Architecture.DependencyInjection;

public static class AddRepositories
{
    public static IServiceCollection RegisterRepositories(this IServiceCollection collection)
    {
        collection.AddScoped<IUnitOfWork, UnitOfWork>();
        collection.AddScoped<IPostsRepository, PostsRepository>();
        collection.AddScoped<ISubriddotsRepository, SubriddotsRepository>();
        collection.AddScoped<IUsersRepository, UsersRepository>();

        return collection;
    } 
}