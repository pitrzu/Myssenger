using Mysennger.Domain;
using Mysennger.Domain.Posts;
using Mysennger.Domain.Subriddots;
using Mysennger.Domain.Users;

namespace Mysennger.Architecture;

public sealed class UnitOfWork : IUnitOfWork
{
    private readonly MsgContext _context;
    
    public UnitOfWork(
        IPostsRepository postsRepository,
        ISubriddotsRepository subriddotsRepository, 
        IUsersRepository usersRepository,
        MsgContext context)
    {
        PostsRepository = postsRepository;
        SubriddotsRepository = subriddotsRepository;
        UsersRepository = usersRepository;
        _context = context;
    }
    
    public IPostsRepository PostsRepository { get; }
    public ISubriddotsRepository SubriddotsRepository { get; }
    public IUsersRepository UsersRepository { get; }
    
    public async Task Commit(CancellationToken cancellationToken = new CancellationToken())
    {
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task Rollback()
    {
        throw new NotImplementedException();
    }
}