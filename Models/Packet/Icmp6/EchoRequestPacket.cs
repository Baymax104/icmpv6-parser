using Models.Field;
using Models.Unit;

namespace Models.Packet.Icmp6;

public class EchoRequestPacket : NetPacket {

    public ushort Identifier {
        get {
            var bytes = new byte[EchoField.IdentifierLength];
            var start = Header.Offset + EchoField.IdentifierPosition;
            Array.Copy(Header.Data, start, bytes, 0, EchoField.IdentifierLength);
            Array.Reverse(bytes);
            return BitConverter.ToUInt16(bytes, 0);
        }
        set {
            var bytes = BitConverter.GetBytes(value);
            Array.Reverse(bytes);
            var start = Header.Offset + EchoField.IdentifierPosition;
            Array.Copy(bytes, 0, Header.Data, start, EchoField.IdentifierLength);
        }
    }

    public ushort SequenceNumber {
        get {
            var bytes = new byte[EchoField.SequenceNumberLength];
            var start = Header.Offset + EchoField.SequenceNumberPosition;
            Array.Copy(Header.Data, start, bytes, 0, EchoField.SequenceNumberLength);
            Array.Reverse(bytes);
            return BitConverter.ToUInt16(bytes, 0);
        }
        set {
            var bytes = BitConverter.GetBytes(value);
            Array.Reverse(bytes);
            var start = Header.Offset + EchoField.SequenceNumberPosition;
            Array.Copy(bytes, 0, Header.Data, start, EchoField.SequenceNumberLength);
        }
    }

    public byte[] Data {
        get => PayloadBytes?.ActualBytes ?? Array.Empty<byte>();
    }

    public EchoRequestPacket(ByteSegment header) : base(header) {
        Header.SegmentLength = EchoField.HeaderLength;
        Payload = ParsePayload();
    }

    protected override sealed Payload ParsePayload() => new(Header.GetNextSegment());
    
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
