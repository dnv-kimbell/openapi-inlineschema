using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;

namespace OpenApi.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Produces("application/json")]
    public class PolymorphicController : ControllerBase
    {
        [HttpPost(Name = "GetPolymorphic")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Pet>))]
        public IActionResult GetPolymorphic(IEnumerable<Pet> body)
        {
            return Ok(body);
        }

        [HttpPost("wrapped", Name = "GetPolymorphicWrapped")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PolymorphicResponse))]
        public IActionResult GetPolymorphicWrapped(PolymorphicRequest body)
        {
            return Ok(new PolymorphicResponse
            {
                Animals = body.Animals
            });
        }
    }

    [JsonPolymorphic(TypeDiscriminatorPropertyName = "$dis")]
    //[JsonDerivedType(typeof(Pet), typeDiscriminator: "pet")]
    [JsonDerivedType(typeof(Cat), typeDiscriminator: "cat")]
    [JsonDerivedType(typeof(Dog), typeDiscriminator: "dog")]
    public class Pet
    {
        public string Name { get; set; } = default!;
    }
    public class Dog : Pet
    {
        public string? Breed { get; set; }
    }

    public class Cat : Pet
    {
        public int? Lives { get; set; }
    }

    public class PolymorphicRequest
    {
        public IEnumerable<Pet> Animals { get; set; } = default!;
    }

    public class PolymorphicResponse
    {
        public IEnumerable<Pet> Animals { get; set; } = default!;
    }
}
