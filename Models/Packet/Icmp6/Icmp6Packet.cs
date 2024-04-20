using Models.Field;
using Models.Packet.Icmp6.Ndp;
using Models.Type;
using Models.Util;

namespace Models.Packet.Icmp6;

public class Icmp6Packet : NetPacket {

    public Icmp6Packet(ByteSegment data) : base(data) {
        Header.SegmentLength = Icmp6Field.HeaderLength;
        Payload = ParsePayload();
    }

    public Icmp6Type Type {
        get => (Icmp6Type)Header[Icmp6Field.TypePosition];
        set => Header[Icmp6Field.TypePosition] = (byte)value;
    }

    public int Code {
        get => Header[Icmp6Field.CodePosition];
        set => Header[Icmp6Field.CodePosition] = (byte)value;
    }

    public ushort Checksum {
        get => Header.ToUInt16(Icmp6Field.ChecksumPosition);
        set => ByteWriter.WriteTo(Header, value, Icmp6Field.ChecksumPosition);
    }

    protected override sealed Payload ParsePayload() {
        var segment = Header.GetNextSegment();
        if (segment.SegmentLength <= 0) {
            return new();
        }
        NetPacket? packet = Type switch {
            Icmp6Type.RouterSolicitation => new RouterSolicitationPacket(segment),
            Icmp6Type.RouterAdvertisement => new RouterAdvertisementPacket(segment),
            Icmp6Type.NeighborAdvertisement => new NeighborAdvertisementPacket(segment),
            Icmp6Type.NeighborSolicitation => new NeighborSolicitationPacket(segment),
            Icmp6Type.EchoRequest => new EchoRequestPacket(segment),
            Icmp6Type.EchoReply => new EchoReplyPacket(segment),
            Icmp6Type.DestinationUnreachable => new DestinationUnreachablePacket(segment),
            Icmp6Type.PacketTooBig => new PacketTooBigPacket(segment),
            Icmp6Type.TimeExceeded => new TimeExceededPacket(segment),
            Icmp6Type.ParameterProblem => new ParameterProblemPacket(segment),
            Icmp6Type.RedirectMessage => new RedirectMessagePacket(segment),
            _ => null
        };
        return packet is not null ? new(packet) : new(segment);
    }

    public override string ToString() {
        return $@"
{{
    {nameof(Type)} = {Type},
    {nameof(Code)} = {Code},
    {nameof(Checksum)} = {Checksum:x}
}}    
    ".Trim();
    }
}
