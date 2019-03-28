using System.ComponentModel.DataAnnotations;

namespace EndGame.Models.UserRequests
{
    public class SubscribeReqModel
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}
