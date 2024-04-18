using Models.Field;
using Models.Type;
using Models.Util;

namespace Models.Packet.Icmp6.Ndp.Option;

public class MtuOption(ByteSegment data) : NdpOption(data) {

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

    public uint Mtu {
        get => PayloadBytes.ToUInt32(MtuOptionField.MtuPosition);
        set => ByteWriter.WriteTo(PayloadBytes, value, MtuOptionField.MtuPosition);
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
