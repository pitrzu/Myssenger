using MediatR;
using Mysennger.Domain.Posts;
using Mysennger.Domain.Posts.ValueObjects;
using OneOf.Monads;

namespace Myssenger.Api.Queries;

public record PostByIdQuery(Guid Id) : IRequest<Result<Exception, Post>>;

public sealed class PostByIdQueryHandler : IRequestHandler<PostByIdQuery, Result<Exception, Post>>
{
    private readonly IPostsRepository _repository;

    public PostByIdQueryHandler(IPostsRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<Exception, Post>> Handle(PostByIdQuery request, CancellationToken cancellationToken)
    {
        var postId = PostId.Create(request.Id);
        var post = await _repository.One(postId); 
        
        return post?? Result<Exception, Post>.Error(new Exception($"Post with id {postId} was not found!"));
    }
}