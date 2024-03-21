using Models.Field;
using Models.Packet.Icmp6.Ndp.Option;
using Models.Unit;

namespace Models.Packet.Icmp6.Ndp;

public class RouterAdvertisementPacket : NdpPacket {

    public RouterAdvertisementPacket(ByteSegment data) : base(data) {
        Header.SegmentLength = NdpField.RAHeaderLength;
    }

    public byte CurHopLimit {
        get => Header[NdpField.RACurHopLimitPosition];
        set => Header[NdpField.RACurHopLimitPosition] = value;
    }

    public bool ManagedAddressConfiguration {
        get => (Header[NdpField.RAMOPosition] & 0x80) != 0;
        set {
            if (value) {
                Header[NdpField.RAMOPosition] |= 0x80;
            } else {
                Header[NdpField.RAMOPosition] &= 0x7F;
            }
        }
    }

    public bool OtherConfiguration {
        get => (Header[NdpField.RAMOPosition] & 0x40) != 0;
        set {
            if (value) {
                Header[NdpField.RAMOPosition] |= 0x40;
            } else {
                Header[NdpField.RAMOPosition] &= 0xBF;
            }
        }
    }

    public ushort RouterLifetime {
        get {
            var bytes = new byte[NdpField.RARouterLifetimeLength];
            var start = Header.Offset + NdpField.RARouterLifetimePosition;
            Array.Copy(Header.Data, start, bytes, 0, NdpField.RARouterLifetimeLength);
            Array.Reverse(bytes);
            return BitConverter.ToUInt16(bytes, 0);
        }
        set {
            var bytes = BitConverter.GetBytes(value);
            Array.Reverse(bytes);
            var start = Header.Offset + NdpField.RARouterLifetimePosition;
            Array.Copy(bytes, 0, Header.Data, start, NdpField.RARouterLifetimeLength);
        }
    }

    public uint ReachableTime {
        get {
            var bytes = new byte[NdpField.RAReachableTimeLength];
            var start = Header.Offset + NdpField.RAReachableTimePosition;
            Array.Copy(Header.Data, start, bytes, 0, NdpField.RAReachableTimeLength);
            Array.Reverse(bytes);
            return BitConverter.ToUInt32(bytes, 0);
        }
        set {
            var bytes = BitConverter.GetBytes(value);
            Array.Reverse(bytes);
            var start = Header.Offset + NdpField.RARouterLifetimePosition;
            Array.Copy(bytes, 0, Header.Data, start, NdpField.RAReachableTimeLength);
        }
    }

    public uint RetransmitTimer {
        get {
            var bytes = new byte[NdpField.RARetransmitTimerLength];
            var start = Header.Offset + NdpField.RARetransmitTimerPosition;
            Array.Copy(Header.Data, start, bytes, 0, NdpField.RARetransmitTimerLength);
            Array.Reverse(bytes);
            return BitConverter.ToUInt32(bytes, 0);
        }
        set {
            var bytes = BitConverter.GetBytes(value);
            Array.Reverse(bytes);
            var start = Header.Offset + NdpField.RARetransmitTimerPosition;
            Array.Copy(bytes, 0, Header.Data, start, NdpField.RARetransmitTimerLength);
        }
    }

    public override List<NdpOption> Options {
        get => ParseOptions(Header.GetNextSegment());
        set => WriteOptions(value, NdpField.RAOptionsPosition);
    }

    public override string ToString() {
        return $@"
{{
    {nameof(CurHopLimit)} = {CurHopLimit},
    {nameof(ManagedAddressConfiguration)} = {ManagedAddressConfiguration},
    {nameof(OtherConfiguration)} = {OtherConfiguration},
    {nameof(RouterLifetime)} = {RouterLifetime},
    {nameof(ReachableTime)} = {ReachableTime},
    {nameof(RetransmitTimer)} = {RetransmitTimer}
    {nameof(Options)} = {PrintOptions()}
}}
        ".Trim();
    }
}
