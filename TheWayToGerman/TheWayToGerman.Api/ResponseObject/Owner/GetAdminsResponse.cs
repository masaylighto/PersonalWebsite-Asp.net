namespace TheWayToGerman.Api.ResponseObject.Owner
{
    public class GetAdminsResponse
    {
        public required Guid Id { get; set; }
        public required string Name { get; set; }

        public required string Username { get; set; }

        public required string Email { get; set; }
    }
}
