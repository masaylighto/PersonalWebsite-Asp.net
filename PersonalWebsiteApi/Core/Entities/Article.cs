namespace PersonalWebsiteApi.Core.Entities;

public class Article : BaseEntity
{
    public required string Title { get; set; }
    public required string Pircture { get; set; } //i should have it stored in the FileSystem instead but currently i have no time for that so it will be TODO!
    public required string Overview { get; set; }
    public required string Content { get; set; }
    public required User Auther { get; set; }
    public required Category Category { get; set; }
}
