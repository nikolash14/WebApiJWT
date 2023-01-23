namespace ESSModels.Model
{
    public class User
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public string? Token { get; set; }
        public string? RoleName { get; set; }
        public Role? UserRole { get; set; }
    }
    //JWT - VGhpcyBpcyBOaWtvbGFzaCwgb3duZXIgb2YgdGhpcyBjdXJyZXdudCBKV1QgQXV0aGVudGljYXRpb24gV2ViIEFQSSBBcHBsaWNhdGlvbi4gVGhpcyBpcyBjb252ZXJ0ZWQgZnJvbSBwbGFpbiB0ZXh0IHRvIGJhc2UgNjQgY29kZS4=
    //FROM - This is Nikolash, owner of this currewnt JWT Authentication Web API Application. This is converted from plain text to base 64 code.
}
