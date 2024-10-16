namespace Web_AppointmentSystem.BUSINESS.Exceptions.CommonExceptions;
internal class EntityAlreadyExistException: Exception
{
    public int StatusCode { get; set; }
    public string PropertyName { get; set; }
    public EntityAlreadyExistException()
    {

    }
    public EntityAlreadyExistException(string? message) : base(message)
    {

    }
    public EntityAlreadyExistException(int statusCode, string propertyName, string? message) : base(message)
    {
        StatusCode = statusCode;
        PropertyName = propertyName;

    }
}
