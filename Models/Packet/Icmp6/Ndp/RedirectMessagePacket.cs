using System.Net;
using Models.Field;
using Models.Packet.Icmp6.Ndp.Option;
using Models.Util;

namespace Models.Packet.Icmp6.Ndp;

public class RedirectMessagePacket : NdpPacket {

    public RedirectMessagePacket(ByteSegment data) : base(data) {
        Header.SegmentLength = NdpField.RMHeaderLength;
        Payload = null;
    }

    public IPAddress TargetAddress {
        get => Header.ToIp6Address(NdpField.RMTargetAddressPosition);
        set => ByteWriter.WriteTo(Header, value, NdpField.RMTargetAddressPosition);
    }

    public IPAddress DestinationAddress {
        get => Header.ToIp6Address(NdpField.RMDestinationAddressPosition);
        set => ByteWriter.WriteTo(Header, value, NdpField.RMDestinationAddressPosition);
    }

    public override List<NdpOption> Options {
        get => ParseOptions(Header.GetNextSegment());
        set => WriteOptions(value, NdpField.RMOptionsPosition);
    }

    public override string ToString() {
        return nameof(RedirectMessagePacket);
    }
}
