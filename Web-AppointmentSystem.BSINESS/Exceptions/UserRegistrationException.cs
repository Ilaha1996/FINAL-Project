public class UserRegistrationException : Exception
{
    public int StatusCode { get; set; }
    public UserRegistrationException()
    {

    }
    public UserRegistrationException(string? message) : base(message)
    {

    }

    public UserRegistrationException(int statusCode, string? message) : base(message)
    {
        StatusCode = statusCode;
    }
}

