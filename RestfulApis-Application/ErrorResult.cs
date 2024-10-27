namespace RestfulApis_Application
{
    public class ErrorResult
    {
        public int StatusCode { get;}
        public List<Error> Errors { get;}
        public ErrorResult(int statusCode, List<Error> errors)
        {
            StatusCode = statusCode;
            Errors = errors;
        }
    }
}
