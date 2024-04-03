using System.Net;
using Models.Field;
using Models.Type;
using Models.Util;

namespace Models.Packet.Icmp6.Ndp.Option;

public class PrefixInformationOption(ByteSegment data) : NdpOption(data) {

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
    
    public byte PrefixLength {
        get => PayloadBytes[PrefixInformationOptionField.PrefixLengthPosition];
        set => PayloadBytes[PrefixInformationOptionField.PrefixLengthPosition] = value;
    }

    public bool OnLink {
        get => (PayloadBytes[PrefixInformationOptionField.LAPosition] & 0x80) != 0;
        set {
            if (value) {
                PayloadBytes[PrefixInformationOptionField.LAPosition] |= 0x80;
            } else {
                PayloadBytes[PrefixInformationOptionField.LAPosition] &= 0x7F;
            }
        }
    }

    public bool AutonomousAddressConfiguration {
        get => (PayloadBytes[PrefixInformationOptionField.LAPosition] & 0x40) != 0;
        set {
            if (value) {
                PayloadBytes[PrefixInformationOptionField.LAPosition] |= 0x40;
            } else {
                PayloadBytes[PrefixInformationOptionField.LAPosition] &= 0xBF;
            }
        }
    }

    public uint ValidLifetime {
        get => PayloadBytes.ToUInt32(PrefixInformationOptionField.ValidLifetimePosition);
        set => ByteWriter.WriteTo(PayloadBytes, value, PrefixInformationOptionField.ValidLifetimePosition);
    }

    public uint PreferredLifetime {
        get => PayloadBytes.ToUInt32(PrefixInformationOptionField.PreferredLifetimePosition);
        set => ByteWriter.WriteTo(PayloadBytes, value, PrefixInformationOptionField.PreferredLifetimePosition);
    }

    public IPAddress Prefix {
        get => PayloadBytes.ToIp6Address(PrefixInformationOptionField.PrefixPosition);
        set => ByteWriter.WriteTo(PayloadBytes, value, PrefixInformationOptionField.PrefixPosition);
    }

    public override string ToString() {
        return $@"
    {{
        {nameof(Type)} = {Type},
        {nameof(Length)} = {Length},
        {nameof(PrefixLength)} = {PrefixLength},
        {nameof(OnLink)} = {OnLink},
        {nameof(AutonomousAddressConfiguration)} = {AutonomousAddressConfiguration},
        {nameof(ValidLifetime)} = {ValidLifetime},
        {nameof(PreferredLifetime)} = {PreferredLifetime},
        {nameof(Prefix)} = {Prefix}
    }}";
    }
}
