using Models.Field;
using Models.Type;
using Models.Util;

namespace Models.Packet.Icmp6.Ndp.Option;

public abstract class NdpOption {

    protected NdpOption(ByteSegment header) {
        Header = header;
        Header.SegmentLength = NdpOptionField.HeaderLength;
        Payload = Header.GetNextSegment();
    }

    protected ByteSegment Header { get; }

    protected ByteSegment Payload { get; }

    public NdpOptionType Type {
        get => (NdpOptionType)Header[NdpOptionField.TypePosition];
        set => Header[NdpOptionField.TypePosition] = (byte)value;
    }

    /// <summary>
    ///     包括Type和Length的整个选项的长度，以8B为单位
    /// </summary>
    public int Length {
        get => Header[NdpOptionField.LengthPosition];
        set => Header[NdpOptionField.LengthPosition] = (byte)value;
    }

    /// <summary>
    ///     当前选项的字节数组
    /// </summary>
    public byte[] ActualBytes {
        get {
            var bytesLength = Length * 8;
            var bytes = new byte[bytesLength];
            Array.Copy(Header.Data, Header.Offset, bytes, 0, bytesLength);
            return bytes;
        }
    }
}
