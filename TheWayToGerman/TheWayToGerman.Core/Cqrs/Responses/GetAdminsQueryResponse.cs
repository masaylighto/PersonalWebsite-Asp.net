namespace TheWayToGerman.Core.Cqrs.Queries;

public class GetAdminsQueryResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }

    public string Username { get; set; }

    public string Email { get; set; }
}
