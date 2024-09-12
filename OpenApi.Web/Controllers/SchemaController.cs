using Microsoft.AspNetCore.Mvc;

namespace OpenApi.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Produces("application/json")]
    public class SchemaController : ControllerBase
    {
        [HttpGet(Name = "Echo")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(EchoData))]
        public IActionResult GetString([FromBody] EchoData body)
        {
            return Ok(body);
        }
    }

    public class EchoData
    {
        public EchoData2? Level2 { get; set; }
    }

    public class EchoData2
    {
        public EchoData3? Level3 { get; set; }
    }

    public class EchoData3
    {
        public string Something { get; set; } = default!;
        public Topic? Topic { get; set; }
        public Customer? Customer { get; set; }
    }

    public class Topic
    {
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
    }

    public class Customer
    {
        public string Name { get; set; } = default!;
        public Address Address { get; set; } = default!;
    }

    public class Address
    {
        public string? Street { get; set; }
        public string? City { get; set; }
    }
}