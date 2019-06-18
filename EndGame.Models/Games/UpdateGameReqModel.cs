using System;

namespace EndGame.Models.Games
{
    public class UpdateGameReqModel
    {
        public string Title { get; set; }

        public object ToEntity(int id)
        {
            throw new NotImplementedException();
        }
    }
}
