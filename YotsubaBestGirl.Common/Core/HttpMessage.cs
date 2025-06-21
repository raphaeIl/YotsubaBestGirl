using Google.Protobuf;
using System;
using YotsubaBestGirl.Common.Utils;

namespace YotsubaBestGirl
{
    public class HttpMessage
    {
        public IMessage Packet { get; init; }

        public Dictionary<string, string>? Headers { get; init; }

        public bool DoGzip { get; set; }

        public HttpMessage(IMessage packet, bool doGzip = false, Dictionary<string, string> customHeaders = null)
        {
            Packet = packet;
            DoGzip = doGzip;

            // init with default headers
            Headers = new Dictionary<string, string>();

            Headers["Proto-Type"] = packet.GetType().FullName.Replace("YotsubaBestGirl.Proto.", "");

            Headers["Connection"] = "keep-alive";
            Headers["Vary"] = "Accept-Encoding";
            Headers["X-Enish-App-Review"] = "false";

            // others
            Headers["X-Enish-Date"] = DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString();
            Headers["Strict-Transport-Security"] = "max-age=15724800; includeSubDomains";

            if (customHeaders is null)
            {
                // these does not exist in some packets, but they does in most of them, so add by default
                Headers["X-Enish-App-Version-Check"] = "1";
                Headers["X-Enish-App-Version-Master"] = Config.MasterVersion.ToString();
                Headers["X-Enish-App-Version-Resource"] = Config.ResourceVersion.ToString();
            }
            
            else
            {
                foreach (var header in customHeaders) // supports overriding
                {
                    Headers[header.Key] = header.Value;
                }
            }
        }

        public static HttpMessage Create(IMessage packet, bool doGzip = false, Dictionary<string, string> customHeaders = null)
        {
            return new HttpMessage(packet, doGzip, customHeaders);
        }


    }
}
