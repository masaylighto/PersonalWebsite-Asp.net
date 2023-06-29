
using Core.Cqrs.Requests;
using Core.DataKit;
using TheWayToGerman.Core.Cqrs.Queries;
using TheWayToGerman.Core.Cqrs.Responses;
using TheWayToGerman.Core.Enums;

namespace TheWayToGerman.Core.Cqrs.Commands;

public class CreateCatagoryCommand : ICommand<CreateCatagoryCommandResponse>
{
    public required string Name { get; set; }
    public required Guid LanguageID { get; set; }
}
