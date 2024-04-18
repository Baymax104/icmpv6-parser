using Models.Field;
using Models.Type;
using Models.Util;

namespace Models.Packet.Icmp6.Ndp.Option;

public class LinkLayerAddressOption(ByteSegment data) : NdpOption(data) {

    public NdpOptionType Type {
        get => (NdpOptionType)Header[NdpOptionField.TypePosition];
        set => Header[NdpOptionField.TypePosition] = (byte)value;
    }

    /// <summary>
    ///     包括Type和Length的整个选项的长度，以8B为单位
    /// </summary>
    public int Length {
        get => Header[NdpOptionField.LengthPosition];
        set => Header[NdpOptionField.LengthPosition] = (byte)value;
    }

    public MacAddress LinkLayerAddress {
        get => PayloadBytes.ToMacAddress(0);
        set => ByteWriter.WriteTo(PayloadBytes, value, 0);
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
