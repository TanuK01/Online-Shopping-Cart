namespace eShoppingZoneAPI.Errors
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


        private string GetDefaultMessageForStatusCode(int statusCode)
        {
            return statusCode switch
            {
                400 => "A Bad request, you have made",
                401 => "Authorized, you are not",
                404 => "Resource found, it was not",
                500 => "Errors are the path to dark side. Error lead to anger and anger laed to hate. Hate leads to carrer changes ;-)",
                _ => null
            };
        }

    }
}
