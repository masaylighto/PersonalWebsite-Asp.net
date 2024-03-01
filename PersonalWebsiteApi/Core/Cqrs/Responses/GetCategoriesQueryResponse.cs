using PersonalWebsiteApi.Core.Entities;
using PersonalWebsiteApi.Core.Enums;

namespace PersonalWebsiteApi.Core.Cqrs.Responses;

public class GetCategoriesQueryResponse
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required Language Language { get; set; }

}
