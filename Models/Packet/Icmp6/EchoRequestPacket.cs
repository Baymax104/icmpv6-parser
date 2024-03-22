using Models.Field;
using Models.Util;

namespace Models.Packet.Icmp6;

public class EchoRequestPacket : NetPacket {

    public EchoRequestPacket(ByteSegment header) : base(header) {
        Header.SegmentLength = EchoField.HeaderLength;
        Payload = ParsePayload();
    }

    public ushort Identifier {
        get => Header.ToUInt16(EchoField.IdentifierPosition);
        set => ByteWriter.WriteTo(Header, value, EchoField.IdentifierPosition);
    }

    public ushort SequenceNumber {
        get => Header.ToUInt16(EchoField.SequenceNumberPosition);
        set => ByteWriter.WriteTo(Header, value, EchoField.SequenceNumberPosition);
    }

    public byte[] Data {
        get => PayloadBytes?.ActualBytes ?? Array.Empty<byte>();
    }

    protected override sealed Payload ParsePayload() {
        var segment = Header.GetNextSegment();
        return segment.SegmentLength > 0 ? new(segment) : new();
    }

    public override string ToString() {
        return $@"
{{
    {nameof(Identifier)} = {Identifier},
    {nameof(SequenceNumber)} = {SequenceNumber},
    {nameof(Data)} = {BitConverter.ToString(Data)}
}}
        ".Trim();
    }
}
