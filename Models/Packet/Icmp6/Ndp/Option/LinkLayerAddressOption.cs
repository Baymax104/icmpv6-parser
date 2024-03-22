using Models.Util;

namespace Models.Packet.Icmp6.Ndp.Option;

internal class LinkLayerAddressOption(ByteSegment data) : NdpOption(data) {

    public MacAddress LinkLayerAddress {
        get => Payload.ToMacAddress(0);
        set => ByteWriter.WriteTo(Payload, value, 0);
    }

    public override string ToString() {
        return $@"
    {{
        {nameof(Type)} = {Type},
        {nameof(Length)} = {Length},
        {nameof(LinkLayerAddress)} = {LinkLayerAddress}
    }}";
    }
}
