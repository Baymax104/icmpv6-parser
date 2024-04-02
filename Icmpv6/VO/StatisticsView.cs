namespace Icmpv6.VO;

public record StatisticsView {
    public uint CapturedPackets { get; set; }

    public uint DroppedPackets { get; set; }

    public uint CapturedProportion { get; set; }

    public uint DroppedProportion { get; set; }
}
