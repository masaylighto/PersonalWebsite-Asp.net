using PersonalWebsiteApi.Core.Entities;

namespace PersonalWebsiteApi.Core.Cqrs.Queries;

public class GetAdminsQueryResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }

    public string Username { get; set; }

    public string Email { get; set; }
    public static GetAdminsQueryResponse From(User user)=> new GetAdminsQueryResponse()
    {
        Email = user.Email,
        Id = user.Id,
        Name = user.Name,
        Username = user.Username
    };
}
