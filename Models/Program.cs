using System.Reflection;
using Models.Packet;
using SharpPcap;
using SharpPcap.LibPcap;

namespace Models;

internal static class Program {

    private static void Main() {
        // var list = LibPcapLiveDeviceList.Instance;
        // foreach (var d in list) Console.WriteLine(d);
        // Read();
        // Write();
    }

    private static void Read() {
        CaptureFileReaderDevice reader = new("echo.pcapng");
        reader.OnPacketArrival += (sender, capture) => {
            var packet = NetPacket.ParsePacket(capture.GetPacket());
            var list = NetPacket.FlatExtract(packet);
            foreach (var p in list) {
                var type = p.GetType();
                var properties = type.GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance);
                foreach (var property in properties) {
                    Console.WriteLine($"{property.Name} -- {property.GetValue(p)}");
                }
            }

        };
        reader.Open();
        reader.StartCapture();
        Console.ReadKey();
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
