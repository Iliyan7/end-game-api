namespace EndGame.Api.TokenProviders.Contracts
{
    public interface ITokenProvider
    {
        string GenerateToken(int id, string email, string[] roles);
    }
}