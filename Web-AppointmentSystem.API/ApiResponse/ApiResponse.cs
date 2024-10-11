namespace Web_AppointmentSystem.API.ApiResponse;

public class ApiResponse<T>
{
    public int StatusCode { get; set; }
    public string ErrorMessage { get; set; }
    public string PropertyName { get; set; }
    public T Data { get; set; }
    public bool IsSuccessful { get => StatusCode >= 200 && StatusCode < 300 ? true : false; }

}
