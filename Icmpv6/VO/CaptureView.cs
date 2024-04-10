using Models.Packet;
using SharpPcap;

namespace Icmpv6.VO;

public record CaptureView {

    private readonly RawCapture? instance;
    public RawCapture Instance {
        get {
            if (instance == null) {
                throw new NotSupportedException("Instance is null.");
            }
            return instance;
        }
    }

    public bool HasInstance {
        get => instance != null;
    }

    public CaptureView() {
    }

    public CaptureView(RawCapture instance) {
        this.instance = instance;
        Length = instance.PacketLength;
        Timestamp = instance.Timeval.Date.ToLongTimeString();
        var packet = NetPacket.ParsePacket(instance);
        var ip6Packet = packet.Extract<Ip6Packet>();
        Source = ip6Packet?.SourceAddress.ToString() ?? "";
        Destination = ip6Packet?.DestinationAddress.ToString() ?? "";
        Protocol = ip6Packet?.NextHeader.ToString() ?? "";
    }

    public int Id { get; init; }

    public string Timestamp { get; set; } = "";

    public string Source { get; set; } = "";

    public string Destination { get; set; } = "";

    public int Length { get; set; }

    public string Protocol { get; set; } = "";
}
