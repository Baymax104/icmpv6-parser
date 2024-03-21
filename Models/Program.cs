using Models.Packet;
using SharpPcap;
using SharpPcap.LibPcap;

namespace Models;

internal static class Program {

    private static void Main(string[] args) {

        CaptureFileReaderDevice reader = new("capture.pcapng");

        reader.OnPacketArrival += OnPacketArrival;

        reader.Open();

        reader.StartCapture();
        Console.ReadLine();

        reader.StopCapture();

        reader.Close();

    }

    private static void OnPacketArrival(object s, PacketCapture capture) {
        var packet = NetPacket.ParsePacket(capture.GetPacket());
        if (packet is not EtherPacket ethernet) {
            return;
        }
        Console.WriteLine("\n----------Ethernet----------");
        Console.WriteLine(ethernet);
        if (ethernet.PayloadPacket is not Ip6Packet ipv6) {
            return;
        }
        Console.WriteLine("----------IPv6----------");
        Console.WriteLine(ipv6);
        if (ipv6.PayloadPacket is not Icmp6Packet icmp6) {
            return;
        }
        Console.WriteLine("----------ICMPv6----------");
        Console.WriteLine(icmp6);
        Console.WriteLine("----------NDP----------");
        Console.WriteLine(icmp6.PayloadPacket);
    }
}
