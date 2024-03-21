using Models.Field;
using Models.Packet.Icmp6.Ndp.Option;
using Models.Unit;

namespace Models.Packet.Icmp6.Ndp;

internal class RouterSolicitationPacket : NdpPacket {

    public RouterSolicitationPacket(ByteSegment data) : base(data) {
        Header.SegmentLength = NdpField.RSHeaderLength;
    }

    public override List<NdpOption> Options {
        get => ParseOptions(Header.GetNextSegment());
        set => WriteOptions(value, NdpField.RSOptionsPosition);
    }

    public override string ToString() {
        return $@"
{{
    {nameof(Options)} = {PrintOptions()}
}}
        ".Trim();
    }
}
