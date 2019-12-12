namespace Carwings.ApiClient.Results
{
    public class InitialResult : IResult
    {
        internal InitialResult()
        {
        }

        public string Baseprm { get; set; }

        public int Status { get; set; }
    }
}