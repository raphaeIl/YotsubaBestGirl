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
        public List<YotsubaPacket> packets = new List<YotsubaPacket>();

        public void LoadAllPackets()
        {
            PcapParser.Instance.Parse("login_packets.json");
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
            string pcapJsonFile = File.ReadAllText(Path.Combine(Config.PcapDir, pcapFileName));
            var data = System.Text.Json.JsonSerializer.Deserialize<List<PcapPacket>>(pcapJsonFile);

            foreach (PcapPacket packet in data)
            {
                // parse packet and add to packet list here
                IMessage parsedPacket = null;

                if (packet.type == "REQUEST")
                {
                    continue;
                }

                byte[] payload = Convert.FromBase64String(packet.payload);

                string protocolNamespace = packet.protocol.Split(".")[0];
                string protocolId = packet.protocol.Split(".")[1];

                Uri uri = new Uri(packet.url);
                string[] segments = uri.AbsolutePath.Split('/', StringSplitOptions.RemoveEmptyEntries);
                string result = string.Join('/', segments[1..]);

                Console.WriteLine("received url: " + result);

                Protocol protocol = Util.GetProtocolFromRoute(result);
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
            File.WriteAllText($"{Config.PcapDir}/{saveFileName}", JsonConvert.SerializeObject(packets, Formatting.Indented));
        }


        public static byte[] ConvertStringToByteArray(string input)
        {
            return input.Trim('[', ']').Split(',').Select(byte.Parse).ToArray();
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
