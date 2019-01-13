using DemoGlobalExceptionHandling.Api.Data;
using DemoGlobalExceptionHandling.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace DemoGlobalExceptionHandling.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly FakeData _data;

        public ValuesController(FakeData data)
        {
            _data = data;
        }

        [HttpGet]
        public ActionResult<int> Get()
        {
            return _data.GetRandomNumber();
        }

        [HttpPost]
        public ActionResult Post(Item item)
        {
            return Ok();
        }
    }
}
