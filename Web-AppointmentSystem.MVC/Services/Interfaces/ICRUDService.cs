namespace Web_AppointmentSystem.MVC.Services.Interfaces
{
    public interface ICRUDService
    {
        Task<T> GetByIdAsync<T>(string endpoint, int id);
        Task<T> GetAllAsync<T>(string endpoint);
        Task Delete<T>(string endpoint, int id);
        Task CreateAsync<T>(string endpoint, T entity) where T : class;
        Task UpdateAsync<T>(string endpoint, T entity) where T : class;
    }
}
