using System.Net;
using Models.Field;
using Models.Util;

namespace Models.Packet.Icmp6.Ndp.Option;

internal class PrefixInformationOption(ByteSegment data) : NdpOption(data) {

    public byte PrefixLength {
        get => Payload[PrefixInformationOptionField.PrefixLengthPosition];
        set => Payload[PrefixInformationOptionField.PrefixLengthPosition] = value;
    }

    public bool OnLink {
        get => (Payload[PrefixInformationOptionField.LAPosition] & 0x80) != 0;
        set {
            if (value) {
                Payload[PrefixInformationOptionField.LAPosition] |= 0x80;
            } else {
                Payload[PrefixInformationOptionField.LAPosition] &= 0x7F;
            }
        }
    }

    public bool AutonomousAddressConfiguration {
        get => (Payload[PrefixInformationOptionField.LAPosition] & 0x40) != 0;
        set {
            if (value) {
                Payload[PrefixInformationOptionField.LAPosition] |= 0x40;
            } else {
                Payload[PrefixInformationOptionField.LAPosition] &= 0xBF;
            }
        }
    }

    public uint ValidLifetime {
        get => Header.ToUInt32(PrefixInformationOptionField.ValidLifetimePosition);
        set => ByteWriter.WriteTo(Header, value, PrefixInformationOptionField.ValidLifetimePosition);
    }

    public uint PreferredLifetime {
        get => Header.ToUInt32(PrefixInformationOptionField.PreferredLifetimePosition);
        set => ByteWriter.WriteTo(Header, value, PrefixInformationOptionField.PreferredLifetimePosition);
    }

    public IPAddress Prefix {
        get => Header.ToIp6Address(PrefixInformationOptionField.PrefixPosition);
        set => ByteWriter.WriteTo(Header, value, PrefixInformationOptionField.PrefixPosition);
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
