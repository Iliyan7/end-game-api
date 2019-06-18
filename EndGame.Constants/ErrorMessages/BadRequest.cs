namespace EndGame.Constants.ErrorMessages
{
    public static class BadRequest
    {
        public static readonly int StatusCode = 400;

        public static readonly (string, string) EmailAlreadyExists = ("0872", "This email already exists.");
        public static readonly (string, string) EmailAlreadySubscribed = ("0872", "This email is already subscribed.");
    }
}
