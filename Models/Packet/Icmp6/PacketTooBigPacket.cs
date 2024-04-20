using Models.Field;
using Models.Util;

namespace Models.Packet.Icmp6;

public class PacketTooBigPacket : NetPacket {

    public PacketTooBigPacket(ByteSegment segment) : base(segment) {
        Header.SegmentLength = ErrorField.HeaderLength;
        Payload = ParsePayload();
    }

    public uint Mtu {
        get => Header.ToUInt32(0);
        set => ByteWriter.WriteTo(Header, value, 0);
    }

    protected override sealed Payload ParsePayload() {
        var segment = Header.GetNextSegment();
        return segment.SegmentLength > 0 ? new(new Ip6Packet(segment)) : new();
    }

    public override string ToString() {
        return nameof(PacketTooBigPacket);
    }
}
