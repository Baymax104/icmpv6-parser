using Models.Field;
using Models.Packet.Ndp;
using Models.Type;
using Models.Unit;

namespace Models.Packet;
internal class Icmp6Packet : NetPacket {

    public Icmp6Type Type {
        get => (Icmp6Type)Header[Icmp6Field.TypePosition];
        set {
            Header[Icmp6Field.TypePosition] = (byte)value;
        }
    }

    public int Code {
        get => Header[Icmp6Field.CodePosition];
        set {
            Header[Icmp6Field.CodePosition] = (byte)value;
        }
    }

    public ushort Checksum {
        get {
            byte[] bytes = new byte[Icmp6Field.ChecksumLength];
            int start = Header.Offset + Icmp6Field.ChecksumPosition;
            Array.Copy(Header.Data, start, bytes, 0, Icmp6Field.ChecksumLength);
            Array.Reverse(bytes);
            return BitConverter.ToUInt16(bytes, 0);
        }
        set {
            byte[] bytes = BitConverter.GetBytes(value);
            Array.Reverse(bytes);
            int start = Header.Offset + Icmp6Field.ChecksumPosition;
            Array.Copy(bytes, 0, Header.Data, start, Icmp6Field.ChecksumLength);
        }
    }

    public Icmp6Packet(ByteSegment data) : base(data) {
        Header.SegmentLength = Icmp6Field.HeaderLength;
        Payload = ParsePayload();
    }

    protected override Payload? ParsePayload() {
        ByteSegment segment = Header.GetNextSegment();
        NetPacket? packet = Type switch {
            Icmp6Type.RouterSolicitation => new RouterSolicitationPacket(segment),
            Icmp6Type.RouterAdvertisement => new RouterAdvertisementPacket(segment),
            _ => null
        };
        return packet is not null ? new(packet) : new(segment);
    }

    public override string ToString() => $@"
{{
    {nameof(Type)} = {Type},
    {nameof(Code)} = {Code},
    {nameof(Checksum)} = {Checksum:x}
}}    
    ".Trim();
}

