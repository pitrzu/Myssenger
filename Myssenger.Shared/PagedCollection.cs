namespace Myssenger.Application;

public sealed class PagedCollection<T> 
    where T : class
{
    public PagedCollection(
        ICollection<T> collection,
        int page,
        int total,
        int lastPage)
    {
        Collection = collection;
        Page = page;
        Total = total;
        LastPage = lastPage;
    }

    public ICollection<T> Collection { get; }
    public int Page { get; }
    public int Total { get; }
    public int LastPage { get; }
}