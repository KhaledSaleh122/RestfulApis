using static System.Runtime.InteropServices.JavaScript.JSType;

namespace RestfulApis_Application
{
    public class Result<TValue>
    {
        public TValue? Value { get; }

        public bool IsSuccess { get; }

        public ErrorResult? ErrorResult { get; }

        public Result(TValue value)
        {
            IsSuccess = true;
            Value = value;
            ErrorResult = default;
        }

        public Result(ErrorResult errorResult)
        {
            IsSuccess = false;
            Value = default;
            ErrorResult = errorResult;
        }
    }
}
