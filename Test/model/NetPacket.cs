using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.model {
    internal abstract class NetPacket(ByteSegment packet) {

        protected ByteSegment Packet { get; } = packet;

        protected ByteSegment Header { get; } = packet;

        protected ByteSegment Payload { get; } = packet;

        public NetPacket(byte[] packet) : this(new ByteSegment(packet)) { }


    }
}
