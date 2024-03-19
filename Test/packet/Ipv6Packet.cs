using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.model;

internal class Ipv6Packet : NetPacket {

    public Ipv6Packet(ByteSegment data) : base(data) { }

    public Ipv6Packet(byte[] data) : base(new ByteSegment(data)) { }

    public override NetPacket ParseNextPacket() {
        throw new NotImplementedException();
    }
}

