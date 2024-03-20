using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.Field;
using Models.Packet.Ndp.Option;
using Models.Unit;

namespace Models.Packet.Ndp;

internal class RouterSolicitationPacket : NdpPacket {

    public override List<NdpOption> Options {
        get => ParseOptions(Header.GetNextSegment());
        set {
            WriteOptions(value, NdpField.RSOptionsPosition);
        }
    }

    public RouterSolicitationPacket(ByteSegment data) : base(data) {
        Header.SegmentLength = NdpField.RSHeaderLength;
    }

}

