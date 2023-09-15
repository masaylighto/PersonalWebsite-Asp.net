
using Core.Cqrs.Requests;
using TheWayToGerman.Core.Cqrs.Responses;
using TheWayToGerman.Core.Enums;

namespace TheWayToGerman.Core.Cqrs.Commands;

public class CreateCategoryCommand : ICommand<CreateCategoryCommandResponse>
{
    public required string Name { get; set; }
    public required Language Language { get; set; } = Language.Arabic;
}
