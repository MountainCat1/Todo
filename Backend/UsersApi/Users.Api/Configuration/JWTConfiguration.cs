namespace Users.Api.Configuration;

public class JWTConfiguration
{
    public string Issuer { get; set; }
    public string Audience { get; set; }
    public string PublicKey { get; set; }
}