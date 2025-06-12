using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace YotsubaBestGirl.SDKServer.Controllers
{
    [ApiController]
    [Route("/")]
    public class SDKController : ControllerBase
    {
        [Route("getConfig")]
        public IResult GetConfig()
        {
            return Results.Text(@"");
        }

        [HttpGet("{*catchAll}")]
        public IResult CatchAllGet(string catchAll)
        {
            Log.Information($"HttpGet: {catchAll}");
            return Results.Empty;
        }

        [HttpPost("{*catchAll}")]
        public IResult CatchAllPost(string catchAll)
        {
            Log.Information($"HttpGet: {catchAll}");
            return Results.Empty;
        }
    }
}

