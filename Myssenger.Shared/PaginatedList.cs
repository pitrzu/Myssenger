namespace Myssenger.Shared;

public sealed class PaginatedList<T> : List<T>
{
    public PaginatedList(IQueryable<T> source, int pageIndex, int pageSize) 
    {
        PageIndex = pageIndex;
        PageSize = pageSize;
        TotalCount = source.Count();
        TotalPages = (int)Math.Ceiling(TotalCount / (double)PageSize);
        
        AddRange(source.Skip(PageIndex * PageSize).Take(PageSize));
    }
    
    public int PageIndex { get; }
    public int PageSize { get; }
    public int TotalCount { get; }
    public int TotalPages { get; }
}