using BLL.Models.WaterBodyModels;
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
    public class WaterBodyController : ControllerBase
    {
        private readonly IWaterBodyService _service;
        public WaterBodyController(IWaterBodyService service)
        {
            _service = service;
        }
        [HttpGet]
        [Route("GetSingleWaterBody")]
        [SwaggerResponse(200, "Success", Type = typeof(WaterBody))]
        [SwaggerResponse(404, "Not Found!")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Bad Request", Type = typeof(SerializableError))]
        [SwaggerOperation(Summary = "Получить элемент по его идентификатору", Description = "Возвращает элемент с указанным идентификатором.Id передается через (параметр запроса)")]
        public async Task<ActionResult<WaterBodyResponse>> GetSingleWaterBody([FromQuery] int id)
        {
            if (id <= 0) { return BadRequest("ID cannot be less than 0 or equal"); }
            var result = await _service.GetSingleWaterBody(id);
            return result == null ? NotFound("No data found from the database!") : Ok(result);
        }
        [Route("GetAllWaterBody")]
        [HttpGet]
        [SwaggerResponse(200, "Success", Type = typeof(List<WaterBody>))]
        [SwaggerResponse(400, "Not Found!")]
        [SwaggerOperation(Summary = "Получить все элемент", Description = "Возвращает все элементы как типа List")]
        public async Task<ActionResult<List<WaterBodyResponse>>> GetAllWaterBody()
        {
            var result = await _service.GetAllWaterBody();
            return result == null ? NotFound("No data found from the database!") : Ok(result);
        }
        [Route("GetAllWaterBodyByRegionId")]
        [HttpGet]
        [SwaggerResponse(200, "Success", Type = typeof(List<WaterBody>))]
        [SwaggerResponse(400, "Not Found!")]
        [SwaggerOperation(Summary = "Получить все элемент по id области", Description = "Возвращает все элементы как типа List")]
        public async Task<ActionResult<List<WaterBodyResponse>>> GetAllWaterBodyByRegionId([FromQuery] int regionId)
        {
            var result = await _service.GetWaterBodyByRegionId(regionId);
            return result == null ? NotFound("No data found from the database!") : Ok(result);
        }

        [Route("CreateWaterBody")]
        [HttpPost]
        [SwaggerResponse(StatusCodes.Status200OK, "Success", Type = typeof(ApiResponse))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Bad Request", Type = typeof(ApiResponse))]
        [SwaggerOperation(Summary = "Создать Элемент", Description = "Создает объект типа WaterBody на основе переданных данных")]

        public async Task<ActionResult<ApiResponse>> AddHouse([FromBody] WaterBodyRequest model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (model == null) { return NoContent(); }
            var result = await _service.CreateWaterBody(model);
            if (result.Success) { return Ok(result); }
            return BadRequest(result);
        }
        [Route("DeleteWaterBody")]
        [HttpDelete]
        [SwaggerResponse(200, "Success", Type = typeof(ApiResponse))]
        [SwaggerResponse(400, "Not Found!")]
        [SwaggerOperation(Summary = "Удалить элемент по его идентификатору", Description = "Удаляет элемент с указанным идентификатором.Id передается через (параметр запроса)")]
        public async Task<ActionResult<ApiResponse>> DeleteWaterBody([FromQuery] int id)
        {
            var result = await _service.DeleteWaterBody(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return NotFound(result);
        }
        [Route("UpdateWaterBody")]
        [HttpPut]
        [SwaggerOperation(Summary = "Обновить элемент", Description = "Обновляет объект типа WaterBody на основе переданных данных, id передается в теле запроса")]
        [SwaggerResponse(StatusCodes.Status200OK, "Success", Type = typeof(ApiResponse))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Bad Request", Type = typeof(SerializableError))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Not Found", Type = typeof(ApiResponse))]
        public async Task<ActionResult<ApiResponse>> UpdateHouse([FromBody] WaterBodyResponse model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (model == null) { return NoContent(); }
            var result = await _service.UpdateWaterBody(model);
            if (result.Success)
            {
                return Ok(result);
            }
            return NotFound(result);
        }
    }
}
