using Ef7FirstLook.Catalog.Header;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ef7FirstLook.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HeaderController : ControllerBase
    {
        private readonly IHeaderService _headerService;
        public HeaderController(IHeaderService headerService)
        {
            _headerService = headerService;
        }

        [HttpGet("All", Name = "GetAllHeader")]
        public async Task<IActionResult> Get()
        {
            var headers = await _headerService.GetAll();
            return Ok(headers);
        }

        [HttpGet("Detail/{id}", Name = "GetDetailHeader")]
        public async Task<IActionResult> GetDetail(int id)
        {
            var headers = await _headerService.GetDetail(id);
            return Ok(headers);
        }

        [HttpGet("Create", Name = "CreateHeader")]
        public async Task<IActionResult> CreateHeader()
        {
            var headers = await _headerService.Create(null);
            return Ok(headers);
        }

        [HttpGet("Update/{id}", Name = "UpdateHeader")]
        public async Task<IActionResult> UpdateHeader(int id)
        {
            var headers = await _headerService.Update(null);
            return Ok(headers);
        }

        [HttpGet("Delete/{id}", Name = "DeleteHeader")]
        public async Task<IActionResult> DeleteHeader(int id)
        {
            var headers = await _headerService.Delete(id);
            return Ok(headers);
        }
    }
}
