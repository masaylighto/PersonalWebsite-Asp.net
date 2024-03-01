
using Core.Cqrs.Requests;
using PersonalWebsiteApi.Core.Cqrs.Responses;
using PersonalWebsiteApi.Core.Enums;

namespace PersonalWebsiteApi.Core.Cqrs.Commands;

public class CreateCategoryCommand : ICommand<CreateCategoryCommandResponse>
{
    public required string Name { get; set; }
    public required Language Language { get; set; } = Language.Arabic;
}
