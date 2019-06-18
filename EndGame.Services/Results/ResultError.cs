using EndGame.Constants;

namespace EndGame.Services.Results
{
    public class ResultError
    {
        public ResultError() : this((Common.NA, Common.NA))
        {
        }

        public ResultError(string description) : this((Common.NA, description))
        {
        }

        public ResultError((string Code, string Description) error)
        {
            this.Code = error.Code;
            this.Description = error.Description;
        }

        
        public string Code { get; set; }

        public string Description { get; set; }
    }
}
