using Models.Field;
using Models.Util;

namespace Models.Packet.Icmp6;

public class TimeExceededPacket : NetPacket {

    public TimeExceededPacket(ByteSegment segment) : base(segment) {
        Header.SegmentLength = ErrorField.HeaderLength;
        Payload = ParsePayload();
    }

    protected override sealed Payload ParsePayload() {
        var segment = Header.GetNextSegment();
        return segment.SegmentLength > 0 ? new(new Ip6Packet(segment)) : new();
    }

    public override string ToString() {
        return nameof(TimeExceededPacket);
    }
}
