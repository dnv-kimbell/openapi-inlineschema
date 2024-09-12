using Microsoft.AspNetCore.Mvc;

namespace OpenApi.Web.Controllers
{
    [ApiController]
    [Route("Test")]
    [Produces("application/json")]
    public class SchemaController : ControllerBase
    {
        [HttpGet("echo", Name = "Echo")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(EchoData))]
        public IActionResult GetString([FromBody] EchoData body)
        {
            return Ok(body);
        }

        [HttpGet("topic", Name = "Topic")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Topic))]
        public IActionResult GetTopic()
        {
            return Ok(new Topic());
        }

        [HttpGet("address", Name = "Addresses")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Address>))]
        public IActionResult GetAddresses()
        {
            return Ok(new List<Address>());
        }

    }

    public class EchoData
    {
        public EchoData2? Level2 { get; set; }
    }

    public class EchoData2
    {
        public EchoData3? Level3 { get; set; }
        public Topic? MyProperty { get; set; }
    }

    public class EchoData3
    {
        public string Something { get; set; } = default!;
        public Topic? TopicTest { get; set; }
        public Customer? Customer { get; set; }
    }

    public class Topic
    {
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public TopicType Type { get; set; }
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

    public enum TopicType
    {
        Public,
        Private
    }
}