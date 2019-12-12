namespace Carwings.ApiClient.Results
{
    public class AsyncResult : IResult
    {
        internal AsyncResult()
        {
        }

        public string ResultKey { get; set; }

        public int Status { get; set; }
    }
}