namespace Authentication.Service.Configuration;

public class JWTConfiguration
{
    public string Issuer { get; set; }
    public string Audience { get; set; }
    public string PublicKey { get; set; }
    public string SecretKey { get; set; }
    public int Expires { get; set; }
}