namespace EndGame.DataAccess.Entities
{
    public class GameImage
    {
        public int GameId { get; set; }
        public Game Game { get; set; }

        public int ImageId { get; set; }
        public Image Image { get; set; }
    }
}