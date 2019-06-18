namespace EndGame.Constants.ErrorMessages
{
    public static class Unauthorized
    {
        public static readonly int StatusCode = 401;

        public static readonly (string, string) InvalidLogin = ("1032", "Login attemp failed.");
    }
}
