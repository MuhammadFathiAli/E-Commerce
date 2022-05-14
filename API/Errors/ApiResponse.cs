namespace API.Errors
{
    public class ApiResponse
    {
        public ApiResponse(int statusCode, string message = null)
        {
            StatusCode = statusCode;
            Message = message ?? GetDefaultMessageForStatusCode(statusCode);
        }

        public int StatusCode { get; set; }
        public string Message { get; set; }
        private string GetDefaultMessageForStatusCode(int statuscode)
        {
            return statuscode switch
            {
                400 => "A bad request, yo have made!",
                401 => "Authorized, U R Not!",
                404 => "Resource Found, it was Not!",
                500 => "Some Server Errors!",
                _ => null
            };
        }
    }
}
