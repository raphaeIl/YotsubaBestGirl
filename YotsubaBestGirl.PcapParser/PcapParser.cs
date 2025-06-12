using Google.Protobuf;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using YotsubaBestGirl.Common.Utils;
using YotsubaBestGirl.Core;
using YotsubaBestGirl.Proto.Proto;

namespace YotsubaBestGirl.PcapParser
{
    public class PcapParser : Singleton<PcapParser>
    {
        public static string ResourceDir = Path.Join(Path.GetDirectoryName(AppContext.BaseDirectory), "Resources");
        public static string PcapDir = Path.Join(ResourceDir, "packets");

        public List<YotsubaPacket> packets = new List<YotsubaPacket>();

        public void LoadAllPackets()
        {
            // these are all the packets i have, should've done more but ran out of time :/
            PcapParser.Instance.Parse("gacha_packets.json");
        }

        public IMessage[] GetAllPcapPacketOfType(Protocol protocol)
        {
            return packets.Where(p => p.Protocol == protocol).Select(x => x.Packet).ToArray();
        }

        public IMessage GetPcapPacket(Protocol protocol)
        {
            return (IMessage)packets.Where(p => p.Protocol == protocol).FirstOrDefault().Packet;
        }

        public YotsubaPacket GetRawPacket(Protocol protocol)
        {
            return packets.Where(p => p.Protocol == protocol).FirstOrDefault();
        }

        public YotsubaPacket[] GetRawPackets(Protocol protocol)
        {
            return packets.Where(p => p.Protocol == protocol).ToArray();
        }

        public void Parse(string pcapFileName)
        {
            string pcapJsonFile = File.ReadAllText(Path.Combine(PcapDir, pcapFileName));
            var data = System.Text.Json.JsonSerializer.Deserialize<List<PcapPacket>>(pcapJsonFile);

            foreach (PcapPacket packet in data)
            {
                // parse packet and add to packet list here
                IMessage parsedPacket = null;
                byte[] payload = Convert.FromBase64String(packet.payload);

                string protocolNamespace = packet.protocol.Split(".")[0];
                string protocolId = packet.protocol.Split(".")[1];

                Uri uri = new Uri(packet.url);
                string[] segments = uri.AbsolutePath.Split('/', StringSplitOptions.RemoveEmptyEntries);
                string result = string.Join('/', segments[1..]);

                Console.WriteLine("received url: " + result);

                string urlProtocol = Util.ConvertToPascalCase(result);
                Console.WriteLine("urlProtocol: " + urlProtocol);
                Protocol protocol = GetRequestProtocolByProtocolName(urlProtocol);
                Console.WriteLine("protocol: " + protocol);

                if (protocol == Protocol.Unknown)
                {
                    Console.WriteLine("invalid imessage type");
                    continue;
                }

                Console.WriteLine($"Got Type: {packet.type}, protocol: {protocol}");

                Type packetClassType = Assembly.GetAssembly(typeof(AccountAuthorize))!.GetTypes().Where(x => x.Namespace == $"YotsubaBestGirl.Proto.{protocolNamespace}" && x.Name == protocolId).SingleOrDefault();

                PropertyInfo parserProperty = packetClassType.GetProperty("Parser", BindingFlags.Static | BindingFlags.Public);
                object parserInstance = parserProperty.GetValue(null);
                MethodInfo parseFromMethod = parserInstance.GetType().GetMethod("ParseFrom", new[] { typeof(byte[]) });

                IMessage parsedMessage = null;

                //try
                //{
                    parsedMessage = (IMessage)parseFromMethod.Invoke(parserInstance, new object[] { payload });
                //} catch (Exception e)
                //{
                //    Console.WriteLine("something went wrong while parsing the proto" + e.Message);
                //    continue;
                //}

                packets.Add(new YotsubaPacket()
                {
                    Packet = parsedMessage,
                    Protocol = protocol,
                    ProtocolName = protocol.ToString(),
                    Payload = payload
                });
            }
        }

        public void SavePackets(string saveFileName)
        {
            Console.WriteLine($"Got {packets.Count} packet(s) out a total of x");
            File.WriteAllText($"{PcapDir}/{saveFileName}", JsonConvert.SerializeObject(packets, Formatting.Indented));
        }


        public static byte[] ConvertStringToByteArray(string input)
        {
            return input.Trim('[', ']').Split(',').Select(byte.Parse).ToArray();
        }

        public static readonly Dictionary<string, Protocol> ProtocolIdToRouteMappings = new Dictionary<string, Protocol>()
        {
            ["AccountAuthorize"] = Protocol.AccountAuthorize,
            ["AccountCertificate"] = Protocol.AccountCertificate,
            ["UserLoad"] = Protocol.UserLoad,
            ["ResourceListAndroid"] = Protocol.ResourceListAndroid,
            ["ShopProducts"] = Protocol.ShopProducts,
            ["FcmToken"] = Protocol.FcmToken,
            ["MasterAll"] = Protocol.MasterAll,
            ["GachaPurchase"] = Protocol.GachaPurchase,
        };

        public static Protocol GetRequestProtocolByProtocolName(string msgId)
        {
            if (!ProtocolIdToRouteMappings.ContainsKey(msgId))
            {
                return Protocol.Unknown;
            }

            Protocol protocolId = ProtocolIdToRouteMappings[msgId];

            return protocolId;
        }
    }

    public class PcapPacket
    {
        public string payload { get; set; }
        public string type { get; set; }
        public string protocol { get; set; }
        public string url { get; set; }
    }

    public class YotsubaPacket
    {
        //public string Method { get; set; }
        public IMessage Packet { get; set; }
        //public string ClassType { get; set; }
        public Protocol Protocol { get; set; }
        public string ProtocolName { get; set; }
        public byte[] Payload { get; set; }
    }
}
