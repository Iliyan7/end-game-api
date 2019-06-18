namespace EndGame.Constants.ErrorMessages
{
    public class NotFound
    {
        public static readonly int StatusCode = 404;

        public static readonly (string, string) NoSuchUser = ("4021", "User with provided Id was not found.");
        public static readonly (string, string) NoSuchGame = ("4022", "Game with provided Id was not found.");
    }
}
