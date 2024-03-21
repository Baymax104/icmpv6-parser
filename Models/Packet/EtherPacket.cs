using Models.Field;
using Models.Type;
using Models.Unit;

namespace Models.Packet;

public class EtherPacket : NetPacket {

    public EtherPacket(ByteSegment data) : base(data) {
        Header.SegmentLength = EtherField.HeaderLength;
        Payload = ParsePayload();
    }

    /// <summary>
    ///     源MAC地址
    /// </summary>
    public MacAddress SourceMacAddress {
        get {
            // 地址长度6B
            var array = new byte[EtherField.MacAddressLength];
            var start = Header.Offset + EtherField.SourceMacPosition;
            Array.Copy(Header.Data, start, array, 0, EtherField.MacAddressLength);
            return new(array);
        }
        set {
            var bytes = value.GetAddressBytes();
            if (bytes.Length != EtherField.MacAddressLength) {
                throw new ArgumentOutOfRangeException(nameof(value));
            }
            var start = Header.Offset + EtherField.SourceMacPosition;
            Array.Copy(bytes, 0, Header.Data, start, EtherField.MacAddressLength);
        }
    }

    /// <summary>
    ///     目的MAC地址
    /// </summary>
    public MacAddress DestinationMacAddress {
        get {
            var array = new byte[EtherField.MacAddressLength];
            var start = Header.Offset + EtherField.DestinationMacPosition;
            Array.Copy(Header.Data, start, array, 0, EtherField.MacAddressLength);
            return new(array);
        }
        set {
            var bytes = value.GetAddressBytes();
            if (bytes.Length != EtherField.MacAddressLength) {
                throw new ArgumentOutOfRangeException(nameof(value));
            }
            var start = Header.Offset + EtherField.DestinationMacPosition;
            Array.Copy(bytes, 0, Header.Data, start, EtherField.MacAddressLength);
        }
    }

    public EtherType Type {
        get {
            // 注意BitConverter转换按机器小端序，而数据包为大端序
            var typeBytes = new byte[2];
            Array.Copy(Header.Data, Header.Offset + EtherField.TypePosition, typeBytes, 0, typeBytes.Length);
            Array.Reverse(typeBytes);
            var type = BitConverter.ToUInt16(typeBytes, 0);
            return (EtherType)type;
        }
        set {
            var type = (ushort)value;
            var typeBytes = BitConverter.GetBytes(type);
            Array.Reverse(typeBytes);
            Array.Copy(typeBytes, 0, Header.Data, Header.Offset + EtherField.TypePosition, typeBytes.Length);
        }
    }


    protected override sealed Payload ParsePayload() {
        var nextSegment = Header.GetNextSegment();
        NetPacket? packet = Type switch {
            EtherType.IPv6 => new Ip6Packet(nextSegment),
            _ => null
        };
        return packet is not null ? new(packet) : new(nextSegment);
    }

    public override string ToString() {
        return $@"
{{
    {nameof(DestinationMacAddress)} = {DestinationMacAddress},
    {nameof(SourceMacAddress)} = {SourceMacAddress},
    {nameof(Type)} = {Type}
}}
        ".Trim();
    }
}
