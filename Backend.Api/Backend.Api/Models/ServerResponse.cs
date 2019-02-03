namespace Backend.Api.Models
{
    public class ServerResponse
    {
        public bool IsSuccess { get; set; }
        public string ErrorMessage { get; set; }

        public static ServerResponse Fail(string errorMessage)
        {
            return new ServerResponse()
            {
                IsSuccess = false,
                ErrorMessage = errorMessage
            };
        }

        public static ServerResponse Success()
        {
            return new ServerResponse()
            {
                IsSuccess = true,
            };
        }
    }

    public class ServerResponse<T> : ServerResponse
    {
        public T Data { get; set; }

        public ServerResponse(T item)
        {
            Data = item;
            IsSuccess = true;
        }

        public ServerResponse()
        {

        }

        public static ServerResponse<T> Success(T item)
        {
            return new ServerResponse<T>(item);
        }

        
    }
}