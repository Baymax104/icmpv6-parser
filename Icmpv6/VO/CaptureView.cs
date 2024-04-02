using Models.Packet;
using SharpPcap;

namespace Icmpv6.VO;

public record CaptureView {

    public RawCapture? Instance { get; set; }

    public CaptureView() {
    }

    public CaptureView(RawCapture instance) {
        Instance = instance;
        Length = instance.PacketLength;
        Timestamp = instance.Timeval.Date.ToShortTimeString();
        var packet = NetPacket.ParsePacket(instance);
        var ip6Packet = packet.Extract<Ip6Packet>();
        Source = ip6Packet?.SourceAddress.ToString() ?? "";
        Destination = ip6Packet?.DestinationAddress.ToString() ?? "";
    }

    public int Index { get; set; } = 0;

    public string Timestamp { get; set; } = "";

    public string Source { get; set; } = "";

    public string Destination { get; set; } = "";

    public int Length { get; set; }
}
