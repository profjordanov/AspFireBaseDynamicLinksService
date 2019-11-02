using System;
using System.Net;
using System.Threading.Tasks;
using FireBaseDynamicLinksService.Services.Core;
using Google.Apis.FirebaseDynamicLinks.v1.Data;
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
        [ProducesResponseType(typeof(CreateShortDynamicLinkResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
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