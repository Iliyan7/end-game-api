namespace EndGame.Constants.ErrorMessages
{
    public static class BadRequest
    {
        public static readonly int StatusCode = 400;

        public static readonly (string, string) EmailAlreadyExists = ("0821", "This email already exists.");
        public static readonly (string, string) EmailAlreadySubscribed = ("0822", "This email is already subscribed.");

        public static readonly (string, string) InvalidGenres = ("0831", "One or more of submitted genres doesn't exists.");
        public static readonly (string, string) InvalidPlatfroms = ("0832", "One or more of submitted platfroms doesn't exists.");
    }
}
