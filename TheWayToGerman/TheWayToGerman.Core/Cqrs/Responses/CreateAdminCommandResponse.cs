using Core.Cqrs.Requests;
using System.ComponentModel.DataAnnotations;
using TheWayToGerman.Core.Entities;
using TheWayToGerman.Core.Enums;

namespace TheWayToGerman.Core.Cqrs.Queries;

public class CreateAdminCommandResponse
{
    public Guid Id { get; set; }
}
