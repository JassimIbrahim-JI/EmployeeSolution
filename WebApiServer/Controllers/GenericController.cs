using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServerLibrary.Repositories.Contracts;

namespace WebApiServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenericController<T> : Controller where T : class
    {
        private readonly IGenaricRepository<T> _genaricRepository;

        public GenericController(IGenaricRepository<T> genaricRepository)
        {
            _genaricRepository = genaricRepository;
        }

        [HttpGet("all")]
        public async Task<IActionResult>GetAll() => Ok(await _genaricRepository.GetAll());

        [HttpDelete("delete/{id:int}")]
        public async Task<IActionResult> Delete(int id) 
        {
            if (id <= 0) return BadRequest("Invalid send request");
            return Ok(await _genaricRepository.DeleteById(id));
        }

        [HttpGet("signle/{id}")]
        public async Task<IActionResult> GetById(int id) 
        {
            if (id <= 0) return BadRequest("Invalid request send");
            return Ok(await _genaricRepository.GetById(id));
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add(T model) 
        {
            if (model is null) return BadRequest("Bad request made");
            return Ok(await _genaricRepository.Insert(model));
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update(T model)
        {
            if (model is null) return BadRequest("Bad request made");
            return Ok(await _genaricRepository.Update(model));
        }
    }
}
