using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace SOQuestion.Controllers
{
    [Route("api/[controller]")]
    public class TestController : Controller
    {
        private const string Name = "Test";
        public const string Add = "Create" + Name;


        private readonly ILogger<TestController> _logger;

        public TestController(ILogger<TestController> logger)
        {
            _logger = logger;
        }

        [HttpPost(Name = Add)]
        [AllowAnonymous]
        public async Task<IActionResult> AddAsync(CancellationToken ct)
        {
            var aModel = new A();
            var bModel = new B();
            if (await TryUpdateModelAsync<A>(aModel))
            {
                _logger.LogDebug($"Model binded to aModel");
                return Ok(aModel);
            }
            else if (await TryUpdateModelAsync<B>(bModel))
            {
                _logger.LogDebug($"Model binded to bModel");
                return Ok(bModel);
            }
            _logger.LogDebug("Nothing binded!");
            return BadRequest();
        }


    }

    public class A
    {
        public int IntPropA { get; set; }
        public string StringPropA { get; set; }
        public string CommonProp { get; set; }
    }

    public class B
    {
        public int IntPropB { get; set; }
        public string StringPropB { get; set; }
        public string CommonProp { get; set; }

    }
}