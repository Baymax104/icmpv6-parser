using Models.Packet;
using Models.Type;

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

    public PayloadType Type {
        get => (packet, bytes) switch {
            (_, null) => PayloadType.Packet,
            (null, _) => PayloadType.Bytes,
            _ => PayloadType.None
        };
    }

    public override string ToString() {
        return Type switch {
            PayloadType.Packet => packet?.ToString() ?? string.Empty,
            PayloadType.Bytes => bytes?.ToString() ?? string.Empty,
            _ => string.Empty
        };
    }
}
