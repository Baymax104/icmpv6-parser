using Models.Field;
using Models.Util;

namespace Models.Packet;

public class UdpPacket : NetPacket {

    public UdpPacket(ByteSegment segment) : base(segment) {
        Header.SegmentLength = UdpField.HeaderLength;
        Payload = ParsePayload();
    }

    public ushort SourcePort {
        get => Header.ToUInt16(UdpField.SourcePortPosition);
        set => ByteWriter.WriteTo(Header, value, UdpField.SourcePortPosition);
    }

    public ushort DestinationPort {
        get => Header.ToUInt16(UdpField.DestinationPortPosition);
        set => ByteWriter.WriteTo(Header, value, UdpField.DestinationPortPosition);
    }

    public ushort Length {
        get => Header.ToUInt16(UdpField.HeaderLengthPosition);
        set => ByteWriter.WriteTo(Header, value, UdpField.HeaderLengthPosition);
    }

    public ushort Checksum {
        get => Header.ToUInt16(UdpField.ChecksumPosition);
        set => ByteWriter.WriteTo(Header, value, UdpField.ChecksumPosition);
    }

    protected override sealed Payload ParsePayload() {
        return new(Header.GetNextSegment());
    }

    public override string ToString() {
        return $@"
{{
    {nameof(SourcePort)} = {SourcePort},
    {nameof(DestinationPort)} = {DestinationPort},
    {nameof(Length)} = {Length},
    {nameof(Checksum)} = {Checksum},
    {nameof(Payload)} = {Payload}
}}
        ".Trim();
    }
}
