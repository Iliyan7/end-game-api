using System.ComponentModel.DataAnnotations;

namespace EndGame.Models.GameRequests
{
    public class CreateGameReqModel
    {
        [Required]
        public string Title { get; set; }
    }
}
