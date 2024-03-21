using Models.Packet;

namespace Models.Unit;

public class Payload {

    private ByteSegment? bytes;

    private NetPacket? packet;

    public Payload() { }

    public Payload(NetPacket packet) {
        this.packet = packet;
    }

    public Payload(ByteSegment bytes) {
        this.bytes = bytes;
    }

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

    public override string ToString() {
        return (packet, bytes) switch {
            (_, null) => packet?.ToString() ?? string.Empty,
            (null, _) => bytes?.ToString() ?? string.Empty,
            _ => string.Empty
        };
    }
}
