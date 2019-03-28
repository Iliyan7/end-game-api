namespace EndGame.Shared.Options
{
    public class TokenProviderOptions
    {
        public TokenProviderOptions()
        {
        }

        public string Issuer { get; set; }

        public string Audience { get; set; }

        public int Expires { get; set; }

        public string IssuerSigningKey { get; set; }
    }
}
