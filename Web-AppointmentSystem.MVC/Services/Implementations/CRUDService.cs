using RestSharp;
using Web_AppointmentSystem.MVC.APIResponseMessages;
using Web_AppointmentSystem.MVC.Services.Interfaces;
using Web_AppointmentSystem.MVC.UIExceptions.Common;

namespace Web_AppointmentSystem.MVC.Services.Implementations
{
    public class CRUDService : ICRUDService
    {
        private readonly RestClient _restclient;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CRUDService(IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
            _restclient = new RestClient(_configuration.GetSection("API:Base_Url").Value);
            var token = _httpContextAccessor.HttpContext.Request.Cookies["token"];

            if (token != null)
            {
                _restclient.AddDefaultHeader("Authorization", "Bearer" + token);
            }

        }
        public async Task CreateAsync<T>(string endpoint, T entity) where T : class
        {
            var request = new RestRequest(endpoint, Method.Post);
            request.AddJsonBody(entity);

            var response = await _restclient.ExecuteAsync<ApiResponseMessage<T>>(request);

            if (!response.IsSuccessful)
            {
                throw new Exception();
            }
        }

        public async Task Delete<T>(string endpoint, int id)
        {
            var request = new RestRequest(endpoint, Method.Delete);
            var response = await _restclient.ExecuteAsync<ApiResponseMessage<T>>(request);

            if (!response.IsSuccessful)
            {
                throw new Exception();
            }
        }

        public async Task<T> GetAllAsync<T>(string endpoint)
        {
            var request = new RestRequest(endpoint, Method.Get);
            var response = await _restclient.ExecuteAsync<ApiResponseMessage<T>>(request);

            if (!response.IsSuccessful)
            {
                throw new Exception();
            }

            return response.Data.Data;
        }

        public async Task<T> GetByIdAsync<T>(string endpoint, int id)
        {
            if (id < 1) throw new Exception();
            var request = new RestRequest(endpoint, Method.Get);
            var response = await _restclient.ExecuteAsync<ApiResponseMessage<T>>(request);

            if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                throw new BadRequestException(response.Data.ErrorMessage);
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                throw new ModelNotFoundException(response.Data.ErrorMessage);
            }
            return response.Data.Data;
        }

        public async Task UpdateAsync<T>(string endpoint, T entity) where T : class
        {
            var request = new RestRequest(endpoint, Method.Put);
            request.AddJsonBody(entity);

            var response = await _restclient.ExecuteAsync<ApiResponseMessage<T>>(request);
            if (!response.IsSuccessful)
            {
                if (response.StatusCode == System.Net.HttpStatusCode.BadRequest && response.Data.PropertyName is not null)
                {
                    throw new ModelStateException(response.Data.PropertyName, response.Data.ErrorMessage);
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    throw new BadRequestException(response.Data.ErrorMessage);
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    throw new ModelNotFoundException(response.Data.ErrorMessage);
                }
            }

        }
    }
}
