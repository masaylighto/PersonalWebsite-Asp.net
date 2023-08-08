
using Core.Cqrs.Requests;
using Core.DataKit;

namespace TheWayToGerman.Core.Cqrs.Commands.Admin;

public class DeleteAdminCommand : ICommand<OK>
{
    public Guid Id { get; set; }
}
