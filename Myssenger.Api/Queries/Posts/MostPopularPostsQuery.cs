using MediatR;
using Mysennger.Domain.Posts;
using Myssenger.Shared;
using OneOf;

namespace Myssenger.Api.Queries.Posts;

public record MostPopularPostsQuery(string Filter, string Sort, int? Page, int? PerPage) : IRequest<OneOf<ArgumentException, Exception, PaginatedList<Post>>>;

public class
    MostPopularPostsQueryHandler : IRequestHandler<MostPopularPostsQuery, OneOf<ArgumentException, Exception, PaginatedList<Post>>>
{
    private readonly IPostsRepository _repository;

    public MostPopularPostsQueryHandler(IPostsRepository repository)
    {
        _repository = repository;
    }

    public async Task<OneOf<ArgumentException, Exception, PaginatedList<Post>>> Handle(MostPopularPostsQuery request, CancellationToken cancellationToken)
    {
        var query = await _repository.PopularPosts(request.Page, request.PerPage);

        if (query.Count == 0)
            return new Exception();
        
        return query;
    }
}