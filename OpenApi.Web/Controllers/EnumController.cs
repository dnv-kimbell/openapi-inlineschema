using Microsoft.AspNetCore.Mvc;

namespace OpenApi.Web.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    [Produces("application/json")]
    public class EnumController : ControllerBase
    {
        [HttpGet("flags", Name = "EnumWithFlags")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(EnumWithFlags))]
        public IActionResult EnumWithFlags(EnumWithFlags e)
        {
            return Ok(e);
        }

        [HttpGet("noflags", Name = "EnumWithNoFlags")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(EnumWithNoFlags))]
        public IActionResult EnumWithNoFlags(EnumWithNoFlags e)
        {
            return Ok(e); ;
        }
    }

    [Flags]
    public enum EnumWithFlags
    {
        None = 0,
        Value2 = 1,
        Value3 = 2,
        Value4 = 4,
        Value5 = 8,
        Value6 = 16
    }

    
    public enum EnumWithNoFlags
    {
        None = 0,
        Value2 = 1,
        Value3 = 2,
        Value4 = 4,
        Value5 = 8,
        Value6 = 16
    }
}
