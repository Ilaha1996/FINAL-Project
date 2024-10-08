namespace Web_AppointmentSystem.BUSINESS.Exceptions.CommonExceptions;

public class InvalidIdException : Exception
{
    public int StatusCode { get; set; }
    public InvalidIdException()
    {

    }
    public InvalidIdException(string? message) : base(message)
    {

    }

    public InvalidIdException(int statusCode, string? message) : base(message)
    {
        StatusCode = statusCode;
    }
}
