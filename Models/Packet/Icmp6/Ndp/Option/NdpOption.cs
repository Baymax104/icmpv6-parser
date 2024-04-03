using Models.Field;
using Models.Util;

namespace Models.Packet.Icmp6.Ndp.Option;

public abstract class NdpOption : NetPacket {

    protected NdpOption(ByteSegment header) : base(header) {
        Header.SegmentLength = NdpOptionField.HeaderLength;
        Payload = ParsePayload();
    }

    protected new ByteSegment PayloadBytes {
        get => Payload?.Bytes ?? throw new NullReferenceException("PayloadBytes is null.");
    }

    protected override sealed Payload ParsePayload() {
        return new(Header.GetNextSegment());
    }

    /// <summary>
    ///     当前选项的字节数组
    /// </summary>
    public byte[] ActualBytes {
        get {
            int length = Header[NdpOptionField.LengthPosition];
            var bytesLength = length * 8;
            var bytes = new byte[bytesLength];
            Array.Copy(Header.Data, Header.Offset, bytes, 0, bytesLength);
            return bytes;
        }
    }
}
