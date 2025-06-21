namespace YotsubaBestGirl.PcapParser
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //PcapParser.Instance.LoadAllPackets();
            PcapParser.Instance.Parse("gacha_packets.json");

            PcapParser.Instance.SavePackets("parsed_gacha_packets.json");
        }
    }
}