using Microsoft.AspNetCore.Mvc;
using Web_AppointmentSystem.API.ApiResponse;
using Web_AppointmentSystem.BUSINESS.DTOs.ServiceDTOs;
using Web_AppointmentSystem.BUSINESS.Exceptions.CommonExceptions;
using Web_AppointmentSystem.BUSINESS.Services.Interfaces;

namespace Web_AppointmentSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServicesController : ControllerBase
    {
        private readonly IServiceService _serviceService;
        public ServicesController(IServiceService serviceService)
        {
            _serviceService = serviceService;
        }

        [HttpGet("")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(new ApiResponse<ICollection<ServiceGetDto>>
            {
                Data = await _serviceService.GetByExpressionAsync(null,true),
                StatusCode = StatusCodes.Status200OK,
                PropertyName = null,
                ErrorMessage = string.Empty,
            });
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ServiceCreateDto dto)
        {
            ServiceGetDto service = null;
            try
            {
                service = await _serviceService.CreateAsync(dto);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<object>
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    ErrorMessage = ex.Message,
                    Data = null
                });
            }
            return Created();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            ServiceGetDto dto = null;
            try
            {
                dto = await _serviceService.GetByIdAsync(id);
            }
            catch (InvalidIdException ex)
            {
                return BadRequest(new ApiResponse<ServiceGetDto>
                {
                    StatusCode = ex.StatusCode,
                    ErrorMessage = ex.Message,
                    Data = null
                });
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(new ApiResponse<ServiceGetDto>
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    ErrorMessage = "Entity not found",
                    Data = null
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<ServiceGetDto>
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    ErrorMessage = ex.Message,
                    Data = null
                });
            }

            return Ok(new ApiResponse<ServiceGetDto>
            {
                Data = dto,
                StatusCode = StatusCodes.Status200OK,
                ErrorMessage = null
            });


        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ServiceUpdateDto dto)
        {
            try
            {
                await _serviceService.UpdateAsync(id, dto);
            }
            catch (InvalidIdException ex)
            {
                return BadRequest(new ApiResponse<ServiceUpdateDto>
                {
                    StatusCode = ex.StatusCode,
                    ErrorMessage = ex.Message,
                    Data = null
                });
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(new ApiResponse<ServiceUpdateDto>
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    ErrorMessage = "Entity not found",
                    Data = null
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<ServiceUpdateDto>
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    ErrorMessage = ex.Message,
                    Data = null
                });
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _serviceService.DeleteAsync(id);
            }
            catch (InvalidIdException ex)
            {
                return BadRequest(new ApiResponse<object>
                {
                    StatusCode = ex.StatusCode,
                    ErrorMessage = ex.Message,
                    Data = null
                });
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(new ApiResponse<object>
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    ErrorMessage = "Entity not found",
                    Data = null
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<object>
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    ErrorMessage = ex.Message,
                    Data = null
                });
            }

            return NoContent();

        }
    }
}


