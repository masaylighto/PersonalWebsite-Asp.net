using Core.Cqrs.Requests;
using System.ComponentModel.DataAnnotations;
using TheWayToGerman.Core.Entities;
using TheWayToGerman.Core.Enums;

namespace TheWayToGerman.Core.Cqrs.Queries;

public class GetAdminsQueryResponse
{
    public Guid Id { get; set; }
    public  string Name { get; set; }

    public  string Username { get; set; }
  
    public  string Email { get; set; }
}
