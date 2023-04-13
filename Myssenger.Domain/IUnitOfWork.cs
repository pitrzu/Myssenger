using Mysennger.Domain.Posts;
using Mysennger.Domain.Subriddots;
using Mysennger.Domain.Users;

namespace Mysennger.Domain;

public interface IUnitOfWork 
{
    public IPostsRepository PostsRepository { get; }
    public ISubriddotsRepository SubriddotsRepository { get; }
    public IUsersRepository UsersRepository { get; }
    
    public Task Commit(CancellationToken cancellationToken = new());
    public Task Rollback();
}