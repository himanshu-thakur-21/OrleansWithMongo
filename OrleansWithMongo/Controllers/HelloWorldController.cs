using Microsoft.AspNetCore.Mvc;
using OrleansWithMongo.Grains.Abstactions;

namespace OrleansWithMongo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HelloWorldController : ControllerBase
    {
        private const string STRING_GRAIN_KEY = "hello_grain_key";
        private readonly Guid GUID_GRAIN_KEY = Guid.Parse("38cd358d7de8445797bf441080fa2583");

        private readonly IClusterClient _clusterClient;

        public HelloWorldController(IClusterClient clusterClient)
        {
            _clusterClient = clusterClient;
        }

        /// <summary>
        /// Run grain locally without persistence
        /// </summary>
        /// <remarks>
        /// This endpoint demonstrates the working of the grain.
        /// The lifecycle of the grain state is same as the application lifecycle
        /// i.e. if you restart you application, the grain state will be reset to default
        /// </remarks>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet("local/{name}")]
        public async Task<IActionResult> HelloWorldLocal(string name)
        {
            var result = await _clusterClient
                .GetGrain<IHelloWorldLocalGrain>(STRING_GRAIN_KEY)
                .SayHelloToAsync(name);

            return Ok(result);
        }

        /// <summary>
        /// This endpoints persists the state using mongo db
        /// </summary>
        /// <remarks>
        /// This endpoint use mongo db to persist the state of the grain.
        /// When you restart your application, it'll load the saved state automatically.
        /// </remarks>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet("db/{name}")]
        public async Task<IActionResult> HelloWorldDb(string name)
        {
            var result = await _clusterClient
                .GetGrain<IHelloWorldDbGrain>(GUID_GRAIN_KEY)
                .SayHelloWorldAsync(name);

            return Ok(result);
        }
    }
}
