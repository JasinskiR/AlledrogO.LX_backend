namespace AlledrogO.Shared.Authentication;

public class JsonWebKey
{
    public string Kid { get; set; }
    public string Kty { get; set; }
    public string N { get; set; }
    public string E { get; set; }
    public string Alg { get; set; }
    public string Use { get; set; }
}