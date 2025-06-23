using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YotsubaBestGirl.Common.Core
{
    public class RequestPacket
    {
        // this is so bad, they have 3 different way of sending data in a request
        // 1. normal http headers, 
        // 2. query params, example: shop/products?all=1    the all=1 is a query param
        // 3. url encoded form data, example: in account/authorize's body, the uuid: xxxxx is the req body (in url encoded format)

        public IDictionary<string, string> Headers { get; set; } = new Dictionary<string, string>();
        public IDictionary<string, string> Query { get; set; } = new Dictionary<string, string>();
        public IDictionary<string, string> Form { get; set; } = new Dictionary<string, string>();

        public static RequestPacket Create(HttpRequest request)
        {
            return new RequestPacket(request);
        }

        public RequestPacket(HttpRequest request)
        {
            Headers = new Dictionary<string, string>();
            Query = new Dictionary<string, string>();
            Form = new Dictionary<string, string>();

            if (request != null)
            {
                foreach (var header in request.Headers)
                {
                    Headers[header.Key] = string.Join(", ", header.Value.ToArray());
                }

                if (request.Query != null)
                {
                    foreach (var (key, value) in request.Query)
                    {
                        Query[key] = value;
                    }
                }

                if (request.HasFormContentType)
                {
                    foreach (var (key, value) in request.Form)
                    {
                        Form[key] = value;
                    }
                }
            }
        }

        public string GetSessionId()
        {
            // X-Enish-App-Session in headers is the sessionId

            if (Headers.TryGetValue("X-Enish-App-Session", out var sessionId))
            {
                return sessionId;
            } else
            {
                throw new InvalidOperationException("Session ID not found in request headers.");
            }
        }
    }
}
