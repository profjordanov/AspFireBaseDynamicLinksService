using System;
using System.Threading.Tasks;
using FireBaseDynamicLinksService.Services.Core;
using Microsoft.AspNetCore.Mvc;

namespace FireBaseDynamicLinksService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DynamicLinksController : ControllerBase
    {
        private readonly IDynamicLinksService _dynamicLinksService;

        public DynamicLinksController(IDynamicLinksService dynamicLinksService)
        {
            _dynamicLinksService = dynamicLinksService;
        }

        [HttpPost]
        public async Task<IActionResult> Post()
        {
            var result = await _dynamicLinksService
                .CreateRoleRequestFireBaseDynamicLinkAsync(
                    Guid.NewGuid().ToString());

            if (result == null)
            {
                return BadRequest();
            }

            return Ok(result);
        }
    }
}