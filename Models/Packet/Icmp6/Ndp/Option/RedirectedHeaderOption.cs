using Models.Field;
using Models.Type;
using Models.Util;

namespace Models.Packet.Icmp6.Ndp.Option;

public class RedirectedHeaderOption(ByteSegment data) : NdpOption(data) {

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

    public byte[] IpHeaderData {
        get {
            var start = PayloadBytes.Offset + RedirectedHeaderOptionField.IpHeaderDataPosition;
            var IpHeaderDataLength = Header.Offset + Length * 8 - start;
            var bytes = new byte[IpHeaderDataLength];
            Array.Copy(PayloadBytes.Data, start, bytes, 0, IpHeaderDataLength);
            return bytes;
        }
        set {
            var start = PayloadBytes.Offset + RedirectedHeaderOptionField.IpHeaderDataPosition;
            var IpHeaderDataLength = Header.Offset + Length * 8 - start;
            Array.Copy(value, 0, PayloadBytes.Data, start, IpHeaderDataLength);
        }
    }

    public override string ToString() {
        return $@"
    {{
        {nameof(Type)} = {Type},
        {nameof(Length)} = {Length},
        {nameof(IpHeaderData)} = [{BitConverter.ToString(IpHeaderData)}]
    }}";
    }
}
