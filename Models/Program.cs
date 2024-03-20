using Models.Packet;
using Models.Packet.Ndp;
using SharpPcap;
using SharpPcap.LibPcap;


namespace Models;
internal class Program {

    static void Main(string[] args) {

        CaptureFileReaderDevice reader = new("capture.pcap");

        reader.OnPacketArrival += new(OnPacketArrival);

        reader.Open();

        reader.StartCapture();
        Console.ReadLine();

        reader.StopCapture();

        reader.Close();

    }

    public static void OnPacketArrival(object s, PacketCapture capture) {
        NetPacket packet = NetPacket.ParsePacket(capture.GetPacket());
        if (packet is EtherPacket ethernet) {
            Console.WriteLine("\n----------Ethernet----------");
            Console.WriteLine(ethernet);
            if (ethernet.PayloadPacket is Ip6Packet ipv6) {
                Console.WriteLine("----------IPv6----------");
                Console.WriteLine(ipv6);
                if (ipv6.PayloadPacket is Icmp6Packet icmp6) {
                    Console.WriteLine("----------ICMPv6----------");
                    Console.WriteLine(icmp6);
                    if (icmp6.PayloadPacket is RouterAdvertisementPacket routerSolicitation) {
                        Console.WriteLine("----------RA----------");
                        Console.WriteLine(routerSolicitation);
                    }
                }
            }
        }
    }

}

