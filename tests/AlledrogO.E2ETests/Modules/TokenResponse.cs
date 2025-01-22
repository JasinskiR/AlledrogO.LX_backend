namespace AlledrogO.E2ETests.Modules;

internal class TokenResponse
{
    public string tokenType { get; set; }
    public string accessToken { get; set; }
    public int expiresIn { get; set; }
    public string refreshToken { get; set; }
}