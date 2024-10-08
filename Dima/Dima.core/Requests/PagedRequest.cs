namespace Dima.core.Requests;

public abstract class PagedRequest : Request
{
    public int PageNumber { get; set; } = Configuration.PageNumber;
    public int PageSize { get; set; } = Configuration.DefaultPageSize;
}