namespace API.Errors
{
    public class ApiException : ApiResponse
    {
        //i extended the functionality of api response class to add details about the exception 
        public ApiException(int statusCode, string message = null, string _details = null) : base(statusCode, message)
        {
            Details = _details;
        }
        public string Details { get; set; }
    }
}
