using Mysennger.Domain.Posts;

namespace Myssenger.Api.Dtos;

public sealed class PostDto 
{
    public PostDto(){}
    public PostDto(Post post)
    {
        Id = post.Id.Value;
        Creator = post.Creator.Value;
        Subriddot = post.Subriddot.Value;
        Title = post.Title.Value;
        ContentText = post.Content.Value;
    }

    public Guid? Id { get; set; }
    public Guid Creator { get; set; }
    public Guid Subriddot { get; set; }
    public string Title { get; set; } = default!;
    public string ContentText { get; set; } = default!;
}