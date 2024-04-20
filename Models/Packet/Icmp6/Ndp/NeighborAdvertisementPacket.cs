using System.Net;
using Models.Field;
using Models.Packet.Icmp6.Ndp.Option;
using Models.Util;

namespace Models.Packet.Icmp6.Ndp;

public class NeighborAdvertisementPacket : NdpPacket {

    public NeighborAdvertisementPacket(ByteSegment data) : base(data) {
        Header.SegmentLength = NdpField.NAHeaderLength;
        Payload = null;
    }

    public bool Router {
        get => (Header[NdpField.NAFlagsPosition] & 0x80) != 0;
        set {
            if (value) {
                Header[NdpField.NAFlagsPosition] |= 0x80;
            } else {
                Header[NdpField.NAFlagsPosition] &= 0x7F;
            }
        }
    }

    public bool Solicited {
        get => (Header[NdpField.NAFlagsPosition] & 0x40) != 0;
        set {
            if (value) {
                Header[NdpField.NAFlagsPosition] |= 0x40;
            } else {
                Header[NdpField.NAFlagsPosition] &= 0xBF;
            }
        }
    }

    public bool Override {
        get => (Header[NdpField.NAFlagsPosition] & 0x20) != 0;
        set {
            if (value) {
                Header[NdpField.NAFlagsPosition] |= 0x20;
            } else {
                Header[NdpField.NAFlagsPosition] &= 0xDF;
            }
        }
    }

    public IPAddress TargetAddress {
        get => Header.ToIp6Address(NdpField.NATargetAddressPosition);
        set => ByteWriter.WriteTo(Header, value, NdpField.NATargetAddressPosition);
    }

    public override List<NdpOption> Options {
        get => ParseOptions(Header.GetNextSegment());
        set => WriteOptions(value, NdpField.NAOptionsPosition);
    }

    public override string ToString() {
        return nameof(NeighborAdvertisementPacket);
    }
}
