using System;

namespace Archangel.Tests.BowlingGame.Infrastructure.Models
{
    public class ServiceResult<T>
    {
        public ServiceResult(T result)
        {
            Result = result;
            IsSuccess = true;
        }

        public ServiceResult(bool isSuccess, string error)
        {
            if (isSuccess)
                throw new ArgumentException("This construcotr is not available for successful result");

            if (string.IsNullOrWhiteSpace(error))
                throw new ArgumentException("Error should be filled");

            IsSuccess = false;
            Error = error;
        }

        public bool IsSuccess { get; }

        public T Result { get; }

        public string Error { get; }
    }
}