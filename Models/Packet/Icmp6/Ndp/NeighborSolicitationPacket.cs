using System.Net;
using Models.Field;
using Models.Packet.Icmp6.Ndp.Option;
using Models.Util;

namespace Models.Packet.Icmp6.Ndp;

public class NeighborSolicitationPacket : NdpPacket {

    public NeighborSolicitationPacket(ByteSegment data) : base(data) {
        Header.SegmentLength = NdpField.NSHeaderLength;
        Payload = null;
    }

    public override List<NdpOption> Options {
        get => ParseOptions(Header.GetNextSegment());
        set => WriteOptions(value, NdpField.NSOptionsPosition);
    }

    public IPAddress TargetAddress {
        get => Header.ToIp6Address(NdpField.NSTargetAddressPosition);
        set => ByteWriter.WriteTo(Header, value, NdpField.NSTargetAddressPosition);
    }

    public override string ToString() {
        return nameof(NeighborSolicitationPacket);
    }
}
