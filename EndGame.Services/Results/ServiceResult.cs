using System.Collections.Generic;

namespace EndGame.Services.Results
{
    public class ServiceResult
    {
        private List<ResultError> errors = new List<ResultError>();

        public bool Succeeded { get; protected set; }

        public object Data { get; protected set; }

        public IEnumerable<ResultError> Errors => errors;

        public static ServiceResult Success(object data = null)
        {
            var result = new ServiceResult { Succeeded = true };
            if(data != null)
            {
                result.Data = data;
            }

            return result;
        }

        public static ServiceResult Failed(params ResultError[] errors)
        {
            var result = new ServiceResult { Succeeded = false };
            if (errors != null)
            {
                result.errors.AddRange(errors);
            }

            return result;
        }
    }
}
