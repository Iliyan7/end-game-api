using System.Collections.Generic;

namespace EndGame.Services.Results
{
    public class ServiceResult
    {
        protected List<ResultError> errors = new List<ResultError>();

        public bool Succeeded { get; protected set; }

        public int StatusCode { get; protected set; }

        public IEnumerable<ResultError> Errors => errors;

        public static ServiceResult Success(object data = null)
        {
            var result = new ServiceResult { Succeeded = true };

            return result;
        }

        public static ServiceResult Failed(int statusCode, params ResultError[] errors)
        {
            var result = new ServiceResult { Succeeded = false, StatusCode = statusCode };
            if (errors != null)
            {
                result.errors.AddRange(errors);
            }

            return result;
        }
    }

    public class ServiceResult<T> : ServiceResult
    {
        public T Data { get; protected set; }

        public static ServiceResult<T> Success(T data)
        {
            var result = new ServiceResult<T> { Succeeded = true, Data = data };
            return result;
        }

        public static new ServiceResult<T> Failed(int statusCode, params ResultError[] errors)
        {
            var result = new ServiceResult<T> { Succeeded = false, StatusCode = statusCode };
            if (errors != null)
            {
                result.errors.AddRange(errors);
            }

            return result;
        }
    }
}
