using Models.Field;
using Models.Type;
using Models.Util;

namespace Models.Packet;

public class EtherPacket : NetPacket {

    public EtherPacket(ByteSegment data) : base(data) {
        Header.SegmentLength = EtherField.HeaderLength;
        Payload = ParsePayload();
    }

    /// <summary>
    ///     目的MAC地址
    /// </summary>
    public MacAddress Destination {
        get => Header.ToMacAddress(EtherField.DestinationMacPosition);
        set => ByteWriter.WriteTo(Header, value, EtherField.DestinationMacPosition);
    }

    /// <summary>
    ///     源MAC地址
    /// </summary>
    public MacAddress Source {
        get => Header.ToMacAddress(EtherField.SourceMacPosition);
        set => ByteWriter.WriteTo(Header, value, EtherField.SourceMacPosition);
    }

    public EtherType Type {
        get => (EtherType)Header.ToUInt16(EtherField.TypePosition);
        set => ByteWriter.WriteTo(Header, (ushort)value, EtherField.TypePosition);
    }


    protected override sealed Payload ParsePayload() {
        var nextSegment = Header.GetNextSegment();
        if (nextSegment.SegmentLength <= 0) {
            return new();
        }
        NetPacket? packet = Type switch {
            EtherType.IPv6 => new Ip6Packet(nextSegment),
            _ => null
        };
        return packet is not null ? new(packet) : new(nextSegment);
    }

    public override string ToString() {
        return nameof(EtherPacket);
    }
}
