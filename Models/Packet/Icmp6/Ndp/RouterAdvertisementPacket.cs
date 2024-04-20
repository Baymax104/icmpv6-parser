using Models.Field;
using Models.Packet.Icmp6.Ndp.Option;
using Models.Util;

namespace Models.Packet.Icmp6.Ndp;

public class RouterAdvertisementPacket : NdpPacket {

    public RouterAdvertisementPacket(ByteSegment data) : base(data) {
        Header.SegmentLength = NdpField.RAHeaderLength;
        Payload = null;
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
        get => Header.ToUInt16(NdpField.RARouterLifetimePosition);
        set => ByteWriter.WriteTo(Header, value, NdpField.RARouterLifetimePosition);
    }

    public uint ReachableTime {
        get => Header.ToUInt16(NdpField.RAReachableTimePosition);
        set => ByteWriter.WriteTo(Header, value, NdpField.RAReachableTimePosition);
    }

    public uint RetransmitTimer {
        get => Header.ToUInt16(NdpField.RARetransmitTimerPosition);
        set => ByteWriter.WriteTo(Header, value, NdpField.RARetransmitTimerPosition);
    }

    public override List<NdpOption> Options {
        get => ParseOptions(Header.GetNextSegment());
        set => WriteOptions(value, NdpField.RAOptionsPosition);
    }

    public override string ToString() {
        return nameof(RouterAdvertisementPacket);
    }
}
