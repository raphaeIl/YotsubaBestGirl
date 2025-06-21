using Google.Protobuf;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System.IO.Compression;
using System.Reflection;
using System.Text;
using System.Text.Json;
using YotsubaBestGirl.Common.Utils;
using YotsubaBestGirl.Core;
using YotsubaBestGirl.GameServer.Controllers.Api.ProtocolHandlers;
using YotsubaBestGirl.PcapParser;
using YotsubaBestGirl.Proto.Proto;

namespace YotsubaBestGirl.GameServer.Controllers.Api
{
    [Route("{Config.GameVersion}/{*path}")] // catch-all segment to capture any route after /GameVersion
    public class GatewayController : ControllerBase
    {
        private readonly IProtocolHandlerFactory protocolHandlerFactory;
        private readonly ILogger<GatewayController> logger;

        public GatewayController(IProtocolHandlerFactory _protocolHandlerFactory, ILogger<GatewayController> _logger)
        {
            protocolHandlerFactory = _protocolHandlerFactory;
            logger = _logger;
        }

        [HttpGet, HttpPost]
        public void PostRequest(string path)
        {
            bool doGzip = false;
            Log.Information("Gateway Post Request from: {path}", path);

            Protocol protocol = Util.GetProtocolFromRoute(path);

            if (protocol == Protocol.Unknown)
            {
                Log.Error("Could not find protocol for path: {path}", path);
            }

            string reqBody;

            using (StreamReader reader = new StreamReader(Request.Body, Encoding.UTF8))
            {
                reqBody = reader.ReadToEnd();
            }

            //Log.Information("reqBody: " + reqBody);

            //Type packetClassType = Assembly.GetAssembly(typeof(AccountAuthorize))!.GetTypes().Where(x => x.Name == protocol.ToString()).SingleOrDefault();

            //IMessage reqPacket = (IMessage)JsonSerializer.Deserialize(reqBody, packetClassType);

            // resp
            var query = HttpContext.Request.Query;

            HttpMessage respMessage = protocolHandlerFactory.Invoke(protocol, query);

            if (respMessage is null)
            {
                Log.Error("Protocol {protocol} is unimplemented and left unhandled", protocol);

                return;
            }

            byte[] respBytes = respMessage.Packet.ToByteArray();

            HttpContext.Response.ContentType = "application/protobuf";

            foreach (var header in respMessage.Headers)
            {
                HttpContext.Response.Headers[header.Key] = header.Value;
            }

            if (respMessage.DoGzip)
            {
                HttpContext.Response.Headers["Content-Encoding"] = "gzip";
                using var gzip = new GZipStream(HttpContext.Response.Body, CompressionLevel.Fastest, leaveOpen: true);
                gzip.Write(respBytes, 0, respBytes.Length);
            } else
            {
                HttpContext.Response.ContentLength = respBytes.Length;
                HttpContext.Response.Body.Write(respBytes, 0, respBytes.Length);
            }
        }
    }
}
