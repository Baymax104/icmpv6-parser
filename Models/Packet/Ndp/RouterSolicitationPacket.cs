using Models.Field;
using Models.Packet.Ndp.Option;
using Models.Unit;

namespace Models.Packet.Ndp;

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
