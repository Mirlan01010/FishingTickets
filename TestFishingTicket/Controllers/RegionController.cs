using BLL.Models.RegionModels;
using BLL.Models.Responses;
using BLL.Services;
using DAL.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel;

namespace TestFishingTicket.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionController : ControllerBase
    {
        private readonly IRegionService _service;
        public RegionController(IRegionService service)
        {
            _service=service;
        }
        [HttpGet]
        [Route("GetSingleRegion")]
        [SwaggerResponse(200, "Success", Type = typeof(Region))]
        [SwaggerResponse(404, "Not Found!")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Bad Request", Type = typeof(SerializableError))]
        [SwaggerOperation(Summary = "Получить элемент по его идентификатору", Description = "Возвращает элемент с указанным идентификатором.Id передается через (параметр запроса)")]
        public async Task<ActionResult<RegionResponse>> GetSingleRegion([FromQuery] int id)
        {
            if (id <= 0) { return BadRequest("ID cannot be less than 0 or equal"); }
            var result = await _service.GetSingleRegion(id);
            return result == null ? NotFound("No data found from the database!") : Ok(result);
        }
        [Route("GetAllRegion")]
        [HttpGet]
        [SwaggerResponse(200, "Success", Type = typeof(List<Region>))]
        [SwaggerResponse(400, "Not Found!")]
        [SwaggerOperation(Summary = "Получить все элемент", Description = "Возвращает все элементы как типа List")]
        public async Task<ActionResult<List<RegionResponse>>> GetAllRegion()
        {
            var result = await _service.GetAllRegion();
            return result == null ? NotFound("No data found from the database!") : Ok(result);
        }
        
        [Route("CreateRegion")]
        [HttpPost]
        [SwaggerResponse(StatusCodes.Status200OK, "Success", Type = typeof(ApiResponse))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Bad Request", Type = typeof(ApiResponse))]
        [SwaggerOperation(Summary = "Создать Элемент", Description = "Создает объект типа Region на основе переданных данных")]

        public async Task<ActionResult<ApiResponse>> AddHouse([FromBody] RegionRequest model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (model == null) { return NoContent(); }
            var result = await _service.CreateRegion(model);
            if (result.Success) { return Ok(result); }
            return BadRequest(result);
        }
        [Route("DeleteRegion")]
        [HttpDelete]
        [SwaggerResponse(200, "Success", Type = typeof(ApiResponse))]
        [SwaggerResponse(400, "Not Found!")]
        [SwaggerOperation(Summary = "Удалить элемент по его идентификатору", Description = "Удаляет элемент с указанным идентификатором.Id передается через (параметр запроса)")]
        public async Task<ActionResult<ApiResponse>> DeleteRegion([FromQuery] int id)
        {
            var result = await _service.DeleteRegion(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return NotFound(result);
        }
        [Route("UpdateRegion")]
        [HttpPut]
        [SwaggerOperation(Summary = "Обновить элемент", Description = "Обновляет объект типа Region на основе переданных данных, id передается в теле запроса")]
        [SwaggerResponse(StatusCodes.Status200OK, "Success", Type = typeof(ApiResponse))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Bad Request", Type = typeof(SerializableError))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Not Found", Type = typeof(ApiResponse))]
        public async Task<ActionResult<ApiResponse>> UpdateHouse([FromBody] RegionResponse model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (model == null) { return NoContent(); }
            var result = await _service.UpdateRegion(model);
            if (result.Success)
            {
                return Ok(result);
            }
            return NotFound(result);
        }
    }
}
