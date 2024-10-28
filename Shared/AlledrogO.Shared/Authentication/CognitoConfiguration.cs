namespace AlledrogO.Shared.Authentication;

public class CognitoConfiguration
{
    public string UserPoolId { get; set; }
    public string Authority { get; set; }
    public string JwksUri => $"{Authority}/.well-known/jwks.json";
}