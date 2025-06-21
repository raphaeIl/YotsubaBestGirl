namespace YotsubaBestGirl.PcapParser
{
    public class Program
    {
        public static void Main(string[] args)
        {
            PcapParser.Instance.LoadAllPackets();

            PcapParser.Instance.SavePackets("parsed_login_packets.json");
        }
    }
}