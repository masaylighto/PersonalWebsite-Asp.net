
using Core.Cqrs.Requests;
using Core.DataKit;
using TheWayToGerman.Core.Enums;

namespace TheWayToGerman.Core.Cqrs.Commands;

public class DeleteAdminCommand : ICommand<OK>
{
   public Guid Id { get; set; } 
}
