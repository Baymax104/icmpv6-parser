using Models.Packet;
using Models.Packet.Icmp6;
using Models.Packet.Icmp6.Ndp;
using SharpPcap;
using SharpPcap.LibPcap;

namespace Models;

internal static class Program {

    private static void Main() {
        Read();
        // Write();
    }

    private static void Read() {
        CaptureFileReaderDevice reader = new("time_exceeded.pcapng");
        reader.OnPacketArrival += (sender, capture) => {
            var packet = NetPacket.ParsePacket(capture.GetPacket());
            var neighbor = packet.Extract<NeighborAdvertisementPacket>();
            Console.WriteLine(neighbor);
            // if (packet is not EtherPacket ethernet) {
            //     return;
            // }
            // Console.WriteLine("\n----------Ethernet----------");
            // Console.WriteLine(ethernet);
            // if (ethernet.PayloadPacket is not Ip6Packet ipv6) {
            //     return;
            // }
            // Console.WriteLine("----------IPv6----------");
            // Console.WriteLine(ipv6);
            // // ipv6.PayloadLength = 20;
            // // Console.WriteLine(ipv6);
            // if (ipv6.PayloadPacket is not Icmp6Packet icmp6) {
            //     return;
            // }
            // Console.WriteLine("----------ICMPv6----------");
            // Console.WriteLine(icmp6);
            // Console.WriteLine($"----------{icmp6.PayloadPacket?.GetType().Name}----------");
            // Console.WriteLine(icmp6.PayloadPacket);
            // Console.WriteLine(icmp6.PayloadPacket?.PayloadPacket);
            // Console.WriteLine(icmp6.PayloadPacket?.PayloadPacket?.PayloadPacket);
        };
        reader.Open();
        reader.StartCapture();
        Console.ReadLine();
        reader.StopCapture();
        reader.Close();
    }

    private static void Write() {
        var writer = new CaptureFileWriterDevice("cap.pcapng");
        var list = LibPcapLiveDeviceList.Instance;
        foreach (var d in list) Console.WriteLine(d.Interface.FriendlyName);
        var i = Console.Read() - '0';
        var device = list[i];
        device.Open(DeviceModes.Promiscuous);
        writer.Open(device);

        device.OnPacketArrival += (_, capture) => writer.Write(capture.GetPacket());

        device.Filter = "icmp6";
        device.StartCapture();

        Console.ReadKey();
        device.StopCapture();
        device.Close();
    }
}
