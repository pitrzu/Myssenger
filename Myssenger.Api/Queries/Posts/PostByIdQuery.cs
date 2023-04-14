using System.Data;
using MediatR;
using Mysennger.Domain.Posts;
using Mysennger.Domain.Posts.ValueObjects;
using OneOf;

namespace Myssenger.Api.Queries.Posts;

public record PostByIdQuery(Guid Id) : IRequest<OneOf<ConstraintException, ArgumentException, Post>>;

public class PostByIdQueryHandler : IRequestHandler<PostByIdQuery, OneOf<ConstraintException, ArgumentException, Post>>
{
    private readonly IPostsRepository _repository;

    public PostByIdQueryHandler(IPostsRepository repository)
    {
        _repository = repository;
    }

    public async Task<OneOf<ConstraintException, ArgumentException, Post>> Handle(PostByIdQuery request,
        CancellationToken cancellationToken)
    {
        var postId = PostId.Create(request.Id);

        var post = await _repository.One(postId);
        return post is not null 
            ? post 
            : new ArgumentException($"Post with id {request.Id} was not found!");
    }
}