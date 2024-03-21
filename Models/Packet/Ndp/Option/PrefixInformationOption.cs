using System.Net;
using Models.Field;
using Models.Unit;

namespace Models.Packet.Ndp.Option;

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
        get {
            var bytes = new byte[PrefixInformationOptionField.ValidLifetimeLength];
            var start = Payload.Offset + PrefixInformationOptionField.ValidLifetimePosition;
            Array.Copy(Payload.Data, start, bytes, 0, PrefixInformationOptionField.ValidLifetimeLength);
            Array.Reverse(bytes);
            return BitConverter.ToUInt32(bytes, 0);
        }
        set {
            var bytes = BitConverter.GetBytes(value);
            Array.Reverse(bytes);
            var start = Payload.Offset + PrefixInformationOptionField.ValidLifetimePosition;
            Array.Copy(bytes, 0, Payload.Data, start, PrefixInformationOptionField.ValidLifetimeLength);
        }
    }

    public uint PreferredLifetime {
        get {
            var bytes = new byte[PrefixInformationOptionField.PreferredLifetimeLength];
            var start = Payload.Offset + PrefixInformationOptionField.PreferredLifetimePosition;
            Array.Copy(Payload.Data, start, bytes, 0, PrefixInformationOptionField.PreferredLifetimeLength);
            Array.Reverse(bytes);
            return BitConverter.ToUInt32(bytes, 0);
        }
        set {
            var bytes = BitConverter.GetBytes(value);
            Array.Reverse(bytes);
            var start = Payload.Offset + PrefixInformationOptionField.PreferredLifetimePosition;
            Array.Copy(bytes, 0, Payload.Data, start, PrefixInformationOptionField.PreferredLifetimeLength);
        }
    }

    public IPAddress Prefix {
        get {
            var span = Payload.AsSpan(PrefixInformationOptionField.PrefixPosition, Ipv6Field.AddressLength);
            return new(span);
        }
        set {
            var bytes = value.GetAddressBytes();
            var start = Payload.Offset + PrefixInformationOptionField.PrefixPosition;
            Array.Copy(bytes, 0, Payload.Data, start, Ipv6Field.AddressLength);
        }
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
