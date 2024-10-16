using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Web_AppointmentSystem.API.ApiResponse;
using Web_AppointmentSystem.BUSINESS.DTOs.ReviewDTOs;
using Web_AppointmentSystem.BUSINESS.DTOs.TimeSlotDTOs;
using Web_AppointmentSystem.BUSINESS.Exceptions.CommonExceptions;
using Web_AppointmentSystem.BUSINESS.Services.Interfaces;

namespace Web_AppointmentSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewsController : ControllerBase
    {
        private readonly IReviewService _reviewService;
        public ReviewsController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        [HttpGet("")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(new ApiResponse<ICollection<ReviewGetDto>>
            {
                Data = await _reviewService.GetByExpressionAsync(null, true),
                StatusCode = StatusCodes.Status200OK,
                PropertyName = null,
                ErrorMessage = string.Empty,
            });
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ReviewCreateDto dto)
        {
            ReviewGetDto review = null;
            try
            {
                review = await _reviewService.CreateAsync(dto);
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
            ReviewGetDto dto = null;
            try
            {
                dto = await _reviewService.GetByIdAsync(id);
            }
            catch (InvalidIdException ex)
            {
                return BadRequest(new ApiResponse<ReviewGetDto>
                {
                    StatusCode = ex.StatusCode,
                    ErrorMessage = ex.Message,
                    Data = null
                });
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(new ApiResponse<ReviewGetDto>
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    ErrorMessage = "Entity not found",
                    Data = null
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<ReviewGetDto>
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    ErrorMessage = ex.Message,
                    Data = null
                });
            }

            return Ok(new ApiResponse<ReviewGetDto>
            {
                Data = dto,
                StatusCode = StatusCodes.Status200OK,
                ErrorMessage = null
            });


        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ReviewUpdateDto dto)
        {
            try
            {
                await _reviewService.UpdateAsync(id, dto);
            }
            catch (InvalidIdException ex)
            {
                return BadRequest(new ApiResponse<ReviewUpdateDto>
                {
                    StatusCode = ex.StatusCode,
                    ErrorMessage = ex.Message,
                    Data = null
                });
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(new ApiResponse<ReviewUpdateDto>
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    ErrorMessage = "Entity not found",
                    Data = null
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<ReviewUpdateDto>
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
                await _reviewService.DeleteAsync(id);
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
