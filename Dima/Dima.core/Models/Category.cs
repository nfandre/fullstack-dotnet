namespace Dima.core.Models;

public class Category
{
    public long Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Descritpion { get; set; } = String.Empty;
    public string UserId { get; set; }  = String.Empty;
}