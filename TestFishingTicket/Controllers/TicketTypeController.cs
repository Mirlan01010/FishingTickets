using BLL.Models.TicketTypeModels;
using BLL.Models.Responses;
using BLL.Services;
using DAL.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace TestFishingTicket.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketTypeController : ControllerBase
    {
        private readonly ITicketTypeService _service;
        public TicketTypeController(ITicketTypeService service)
        {
            _service = service;
        }
        [HttpGet]
        [Route("GetSingleTicketType")]
        [SwaggerResponse(200, "Success", Type = typeof(TicketType))]
        [SwaggerResponse(404, "Not Found!")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Bad Request", Type = typeof(SerializableError))]
        [SwaggerOperation(Summary = "Получить элемент по его идентификатору", Description = "Возвращает элемент с указанным идентификатором.Id передается через (параметр запроса)")]
        public async Task<ActionResult<TicketTypeResponse>> GetSingleTicketType([FromQuery] int id)
        {
            if (id <= 0) { return BadRequest("ID cannot be less than 0 or equal"); }
            var result = await _service.GetSingleTicketType(id);
            return result == null ? NotFound("No data found from the database!") : Ok(result);
        }
        [Route("GetAllTicketType")]
        [HttpGet]
        [SwaggerResponse(200, "Success", Type = typeof(List<TicketType>))]
        [SwaggerResponse(400, "Not Found!")]
        [SwaggerOperation(Summary = "Получить все элемент", Description = "Возвращает все элементы как типа List")]
        public async Task<ActionResult<List<TicketTypeResponse>>> GetAllTicketType()
        {
            var result = await _service.GetAllTicketType();
            return result == null ? NotFound("No data found from the database!") : Ok(result);
        }

        [Route("CreateTicketType")]
        [HttpPost]
        [SwaggerResponse(StatusCodes.Status200OK, "Success", Type = typeof(ApiResponse))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Bad Request", Type = typeof(ApiResponse))]
        [SwaggerOperation(Summary = "Создать Элемент", Description = "Создает объект типа TicketType на основе переданных данных")]

        public async Task<ActionResult<ApiResponse>> AddHouse([FromBody] TicketTypeRequest model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (model == null) { return NoContent(); }
            var result = await _service.CreateTicketType(model);
            if (result.Success) { return Ok(result); }
            return BadRequest(result);
        }
        [Route("DeleteTicketType")]
        [HttpDelete]
        [SwaggerResponse(200, "Success", Type = typeof(ApiResponse))]
        [SwaggerResponse(400, "Not Found!")]
        [SwaggerOperation(Summary = "Удалить элемент по его идентификатору", Description = "Удаляет элемент с указанным идентификатором.Id передается через (параметр запроса)")]
        public async Task<ActionResult<ApiResponse>> DeleteTicketType([FromQuery] int id)
        {
            var result = await _service.DeleteTicketType(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return NotFound(result);
        }
        [Route("UpdateTicketType")]
        [HttpPut]
        [SwaggerOperation(Summary = "Обновить элемент", Description = "Обновляет объект типа TicketType на основе переданных данных, id передается в теле запроса")]
        [SwaggerResponse(StatusCodes.Status200OK, "Success", Type = typeof(ApiResponse))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Bad Request", Type = typeof(SerializableError))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Not Found", Type = typeof(ApiResponse))]
        public async Task<ActionResult<ApiResponse>> UpdateHouse([FromBody] TicketTypeResponse model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (model == null) { return NoContent(); }
            var result = await _service.UpdateTicketType(model);
            if (result.Success)
            {
                return Ok(result);
            }
            return NotFound(result);
        }
    }
}
