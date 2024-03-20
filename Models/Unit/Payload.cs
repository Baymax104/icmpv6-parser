using Models.Constant;
using Models.Packet;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Unit;
internal class Payload {

    private NetPacket? packet;

    private ByteSegment? bytes;

    public NetPacket? Packet {
        get => packet;
        set {
            bytes = null;
            packet = value;
        }
    }

    public ByteSegment? Bytes {
        get => bytes;
        set {
            packet = null;
            bytes = value;
        }
    }

    public PayloadType Type {
        get => (packet, bytes) switch {
            (_, null) => PayloadType.Packet,
            (null, _) => PayloadType.Bytes,
            _ => PayloadType.None,
        };
    }

    public Payload() { }

    public Payload(NetPacket packet) {
        this.packet = packet;
    }

    public Payload(ByteSegment bytes) {
        this.bytes = bytes;
    }

    public override string ToString() {
        return Type switch {
            PayloadType.Packet => packet?.ToString() ?? string.Empty,
            PayloadType.Bytes => bytes?.ToString() ?? string.Empty,
            _ => string.Empty
        };
    }
}

