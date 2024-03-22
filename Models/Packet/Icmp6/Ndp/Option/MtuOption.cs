using Models.Field;
using Models.Util;

namespace Models.Packet.Icmp6.Ndp.Option;

internal class MtuOption(ByteSegment data) : NdpOption(data) {

    public uint Mtu {
        get => Header.ToUInt32(MtuOptionField.MtuPosition);
        set => ByteWriter.WriteTo(Header, value, MtuOptionField.MtuPosition);
    }

    public override string ToString() {
        return $@"
    {{
        {nameof(Type)} = {Type},
        {nameof(Length)} = {Length},
        {nameof(Mtu)} = {Mtu}
    }}";
    }
}
