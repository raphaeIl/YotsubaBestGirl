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

            Log.Information("reqBody: " + reqBody);

            //Type packetClassType = Assembly.GetAssembly(typeof(AccountAuthorize))!.GetTypes().Where(x => x.Name == protocol.ToString()).SingleOrDefault();

            //IMessage reqPacket = (IMessage)JsonSerializer.Deserialize(reqBody, packetClassType);

            // resp
            var query = HttpContext.Request.Query;
            IMessage? respPacket = protocolHandlerFactory.Invoke(protocol, query);

            if (respPacket is null)
            {
                Log.Error("Protocol {protocol} is unimplemented and left unhandled", protocol);

                return;
            }

            byte[] respBytes = respPacket.ToByteArray();

            //if (protocol == Protocol.UserLoad || protocol == Protocol.FcmToken || protocol == Protocol.AccountCertificate || protocol == Protocol.AccountAuthorize)
            //{
            //    respBytes = PcapParser.PcapParser.Instance.GetRawPacket(protocol).Payload;
            //    Log.Information($"returning raw bytes for {protocol.ToString()} of size: " + respBytes.Length);
            //}

            //else if (protocol == Protocol.ShopProducts)
            //{
            //    doGzip = true;
            //    string all = query["all"];

            //    Log.Information("Received ShopProducts with all: " + all);
            //    YotsubaPacket[] pcapPackets = PcapParser.PcapParser.Instance.GetRawPackets(Protocol.ShopProducts);

            //    if (all == "1")
            //    {
            //        respBytes = pcapPackets[0].Payload;
            //    }
            //    else if (all == "0")
            //    {
            //        respBytes = pcapPackets[1].Payload;
            //    }
            //}

            //if (respBytes.Length > 1000 || doGzip)
            //{
            //    using (var output = new MemoryStream())
            //    {
            //        using (var gzip = new GZipStream(output, CompressionLevel.Fastest, leaveOpen: true))
            //        {
            //            gzip.Write(respBytes, 0, respBytes.Length);
            //        }
            //        respBytes = output.ToArray();
            //    }

            //    HttpContext.Response.Headers["Content-Encoding"] = "gzip";
            //}

            HttpContext.Response.ContentType = "application/protobuf";
            HttpContext.Response.Headers["Proto-Type"] = respPacket.GetType().FullName.Replace("YotsubaBestGirl.Proto.", "");

            HttpContext.Response.Headers["Connection"] = "keep-alive";
            HttpContext.Response.Headers["Vary"] = "Accept-Encoding";
            HttpContext.Response.Headers["X-Enish-App-Review"] = "false";

            if (protocol == Protocol.resource_list_Android)
            {
                HttpContext.Response.Headers["X-Enish-App-Resource-Cnt"] = "54055";
            }

            else
            {
                HttpContext.Response.Headers["X-Enish-App-Version-Check"] = "1";
                HttpContext.Response.Headers["X-Enish-App-Version-Master"] = Config.MasterVersion.ToString();
                HttpContext.Response.Headers["X-Enish-App-Version-Resource"] = Config.ResourceVersion.ToString();
            }

            
            HttpContext.Response.Headers["X-Enish-Date"] = DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString();
            HttpContext.Response.Headers["Strict-Transport-Security"] = "max-age=15724800; includeSubDomains";

            if (respBytes.Length > 1000 || doGzip)
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
