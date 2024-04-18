using BLL.Models.CitizenShipModels;
using BLL.Models.Responses;
using BLL.Services;
using DAL.Entities;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace TestFishingTicket.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CitizenShipController : ControllerBase
    {
        private readonly ICitizenShipService _service;
        public CitizenShipController(ICitizenShipService service)
        {
            _service = service;
        }
        [HttpGet]
        [Route("GetSingleCitizenShip")]
        [SwaggerResponse(200, "Success", Type = typeof(CitizenShipResponse))]
        [SwaggerResponse(404, "Not Found!")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Bad Request", Type = typeof(SerializableError))]
        [SwaggerOperation(Summary = "Получить элемент по его идентификатору", Description = "Возвращает элемент с указанным идентификатором.Id передается через (параметр запроса)")]
        public async Task<ActionResult<CitizenShipResponse>> GetSingleCitizenShip([FromQuery] int id)
        {   if(id <= 0) { return BadRequest("ID cannot be less than 0 or equal"); }
            var result = await _service.GetSingleCitizenShip(id);
            return result == null ? NotFound("No data found from the database!") : Ok(result);
        }
        [Route("GetAllCitizenShip")]
        [HttpGet]
        [SwaggerResponse(200, "Success", Type = typeof(List<CitizenShip>))]
        [SwaggerResponse(400, "Not Found!")]
        [SwaggerOperation(Summary = "Получить все элемент", Description = "Возвращает все элементы как типа List")]
        public async Task<ActionResult<List<CitizenShipResponse>>> GetAllCitizenShip()
        {
            var result = await _service.GetAllCitizenShip();
            return result == null ? NotFound("No data found from the database!") : Ok(result);
        }

        [Route("CreateCitizenShip")]
        [HttpPost]
        [SwaggerResponse(StatusCodes.Status200OK, "Success", Type = typeof(ApiResponse))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Bad Request", Type = typeof(ApiResponse))]
        [SwaggerOperation(Summary = "Создать Элемент", Description = "Создает объект типа CitizenShip на основе переданных данных")]

        public async Task<ActionResult<ApiResponse>> AddHouse([FromBody] CitizenShipRequest model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (model == null) { return NoContent(); }
            var result = await _service.CreateCitizenShip(model);
            if (result.Success) { return Ok(result); }
            return BadRequest(result);
        }
        [Route("DeleteCitizenShip")]
        [HttpDelete]
        [SwaggerResponse(200, "Success", Type = typeof(ApiResponse))]
        [SwaggerResponse(400, "Not Found!")]
        [SwaggerOperation(Summary = "Удалить элемент по его идентификатору", Description = "Удаляет элемент с указанным идентификатором.Id передается через (параметр запроса)")]
        public async Task<ActionResult<ApiResponse>> DeleteCitizenShip([FromQuery] int id)
        {
            var result = await _service.DeleteCitizenShip(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return NotFound(result);
        }
        [Route("UpdateCitizenShip")]
        [HttpPut]
        [SwaggerOperation(Summary = "Обновить элемент", Description = "Обновляет объект типа CitizenShip на основе переданных данных, id передается в теле запроса")]
        [SwaggerResponse(StatusCodes.Status200OK, "Success", Type = typeof(ApiResponse))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Bad Request", Type = typeof(SerializableError))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Not Found", Type = typeof(ApiResponse))]
        public async Task<ActionResult<ApiResponse>> UpdateHouse([FromBody] CitizenShipResponse model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (model == null) { return NoContent(); }
            var result = await _service.UpdateCitizenShip(model);
            if (result.Success)
            {
                return Ok(result);
            }
            return NotFound(result);
        }
    }
}
