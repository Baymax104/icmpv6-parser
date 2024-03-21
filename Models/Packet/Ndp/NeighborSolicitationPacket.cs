using System.Net;
using Models.Field;
using Models.Packet.Ndp.Option;
using Models.Unit;

namespace Models.Packet.Ndp;

public class NeighborSolicitationPacket : NdpPacket {

    public NeighborSolicitationPacket(ByteSegment data) : base(data) {
        Header.SegmentLength = NdpField.NSHeaderLength;
    }

    public override List<NdpOption> Options {
        get => ParseOptions(Header.GetNextSegment());
        set => WriteOptions(value, NdpField.NSOptionsPosition);
    }

    public IPAddress TargetAddress {
        get {
            var span = Header.AsSpan(NdpField.NSTargetAddressPosition, Ipv6Field.AddressLength);
            return new(span);
        }
        set {
            var bytes = value.GetAddressBytes();
            var start = Header.Offset + NdpField.NSTargetAddressPosition;
            Array.Copy(bytes, 0, Header.Data, start, Ipv6Field.AddressLength);
        }
    }

    public override string ToString() {
        return $@"
{{
    {nameof(TargetAddress)} = {TargetAddress},
    {nameof(Options)} = {PrintOptions()}
}}
        ".Trim();
    }
}
