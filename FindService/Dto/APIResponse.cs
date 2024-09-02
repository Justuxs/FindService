namespace FindService.Dto
{
    public class APIResponse<T>
    {
        /// <summary>
        /// Indicates whether the API request was successful.
        /// </summary>
        public bool IsSuccess { get; set; }

        /// <summary>
        /// Contains any data returned by the API request.
        /// </summary>
        public T Data { get; set; }

        /// <summary>
        /// Contains an error message if the API request was not successful.
        /// </summary>
        public string ErrorMessage { get; set; }

        /// <summary>
        /// Initializes a successful API response with data.
        /// </summary>
        /// <param name="data">The data to return.</param>
        public APIResponse(T data)
        {
            IsSuccess = true;
            Data = data;
            ErrorMessage = null;
        }

        /// <summary>
        /// Initializes an unsuccessful API response with an error message.
        /// </summary>
        /// <param name="errorMessage">The error message to return.</param>
        public APIResponse(string errorMessage)
        {
            IsSuccess = false;
            Data = default;
            ErrorMessage = errorMessage;
        }

        /// <summary>
        /// Initializes an empty API response.
        /// </summary>
        public APIResponse()
        {
            IsSuccess = false;
            Data = default;
            ErrorMessage = "An unexpected error occurred.";
        }
    }
}
