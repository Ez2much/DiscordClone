using System.Runtime.InteropServices;
using DiscordClone.Utils;
using Microsoft.AspNetCore.Mvc;

namespace DiscordClone.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BaseController : ControllerBase
    {
        protected IActionResult HandleResult<T>(Result<T> result)
        {
            if (!result.IsSuccess)
            {
                return BadRequest(result.Message);
            }
            return Ok(result.Data);
        }
    }
}