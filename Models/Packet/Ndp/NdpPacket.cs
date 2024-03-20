using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.Unit;

namespace Models.Packet.Ndp;
internal abstract class NdpPacket(ByteSegment header) : NetPacket(header) {

    protected List<Option> Options {
        get {

        }
        set {

        }
    }

}

