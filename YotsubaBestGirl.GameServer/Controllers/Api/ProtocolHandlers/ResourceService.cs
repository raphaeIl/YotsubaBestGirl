using Google.Protobuf;
using Newtonsoft.Json;
using System.IO;
using System.Net.Sockets;
using System.Reflection;
using YotsubaBestGirl.Common.Utils;
using YotsubaBestGirl.Core;
using YotsubaBestGirl.Proto.Pcommon;
using YotsubaBestGirl.Proto.Pmaster;
using YotsubaBestGirl.Proto.Proto;

namespace YotsubaBestGirl.GameServer.Controllers.Api.ProtocolHandlers
{
    public class ResourceService
    {
        // this file is bascially just pcap in base64 format because its so fucking big, 
        // this game is so dogshit they store tables in their packets, so we probably need to make a custom table loader system
        public string ResourceDataPath = Path.Combine(Config.ResourceDir, "resource_data.json");

        private Dictionary<Protocol, IMessage> resourcePreload;

        public ResourceService(ILogger<ResourceService> _logger)
        {
            resourcePreload = new Dictionary<Protocol, IMessage>();

            LoadResources();
        }

        public T GetResource<T>(Protocol type) where T : IMessage
        {
            if (resourcePreload.TryGetValue(type, out IMessage resource))
            {
                return (T)resource;
            }

            return default(T);
        }

        private void LoadResources()
        {
            string resourceDataStr = File.ReadAllText(ResourceDataPath);
            var dataList = JsonConvert.DeserializeObject<List<ResourceData>>(resourceDataStr);

            foreach (var resource in dataList)
            {
                Protocol dataType = Enum.Parse<Protocol>(resource.Type);

                byte[] data = Convert.FromBase64String(resource.Data);

                string protocolNamespace = resource.ProtocolType.Split(".")[0];
                string protocolId = resource.ProtocolType.Split(".")[1];

                Type packetClassType = Assembly.GetAssembly(typeof(AccountAuthorize))!.GetTypes().Where(x => x.Namespace == $"YotsubaBestGirl.Proto.{protocolNamespace}" && x.Name == protocolId).SingleOrDefault();

                PropertyInfo parserProperty = packetClassType.GetProperty("Parser", BindingFlags.Static | BindingFlags.Public);
                object parserInstance = parserProperty.GetValue(null);
                MethodInfo parseFromMethod = parserInstance.GetType().GetMethod("ParseFrom", new[] { typeof(byte[]) });

                IMessage parsedData = (IMessage)parseFromMethod.Invoke(parserInstance, new object[] { data });

                resourcePreload.Add(dataType, parsedData);
            }
        }

        public class ResourceData
        {
            [JsonProperty("data")]
            public string Data { get; set; }

            [JsonProperty("type")]
            public string Type { get; set; }

            [JsonProperty("protocolType")]
            public string ProtocolType { get; set; }
        }
    }

    internal static class ResourceServiceExtensions
    {
        public static void AddResourceService(this IServiceCollection services)
        {
            services.AddSingleton<ResourceService>();
        }
    }
}
