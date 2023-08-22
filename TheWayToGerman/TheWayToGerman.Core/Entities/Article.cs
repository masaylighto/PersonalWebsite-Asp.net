namespace TheWayToGerman.Core.Entities;

public class Article : BaseEntity
{
    public required string Title { get; set; }
    public required string Overview { get; set; }
    public required string Content { get; set; }
    public required User Auther { get; set; }
    public required Category Category { get; set; }
}
