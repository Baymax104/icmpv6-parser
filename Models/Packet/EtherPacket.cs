using Models.Constant;
using Models.Unit;

namespace Models.Packet;
internal class EtherPacket : NetPacket {

    /// <summary>
    /// 源MAC地址
    /// </summary>
    public MacAddress SourceMacAddress {
        get {
            // 地址长度6B
            byte[] array = new byte[EtherFields.MacAddressLength];
            int start = Header.Offset + EtherFields.SourceMacPosition;
            Array.Copy(Header.Data, start, array, 0, EtherFields.MacAddressLength);
            return new(array);
        }
        set {
            byte[] bytes = value.GetAddressBytes();
            if (bytes.Length != EtherFields.MacAddressLength) {
                throw new ArgumentOutOfRangeException(nameof(value));
            }
            int start = Header.Offset + EtherFields.SourceMacPosition;
            Array.Copy(bytes, 0, Header.Data, start, EtherFields.MacAddressLength);
        }
    }

    /// <summary>
    /// 目的MAC地址
    /// </summary>
    public MacAddress DestinationMacAddress {
        get {
            byte[] array = new byte[EtherFields.MacAddressLength];
            int start = Header.Offset + EtherFields.DestinationMacPosition;
            Array.Copy(Header.Data, start, array, 0, EtherFields.MacAddressLength);
            return new(array);
        }
        set {
            byte[] bytes = value.GetAddressBytes();
            if (bytes.Length != EtherFields.MacAddressLength) {
                throw new ArgumentOutOfRangeException(nameof(value));
            }
            int start = Header.Offset + EtherFields.DestinationMacPosition;
            Array.Copy(bytes, 0, Header.Data, start, EtherFields.MacAddressLength);
        }
    }

    public EtherType Type {
        get {
            // 注意BitConverer转换按机器小端序，而数据包为大端序
            byte[] typeBytes = new byte[2];
            Array.Copy(Header.Data, Header.Offset + EtherFields.TypePosition, typeBytes, 0, typeBytes.Length);
            Array.Reverse(typeBytes);
            ushort type = (ushort)BitConverter.ToInt16(typeBytes, 0);
            return (EtherType)type;
        }
        set {
            ushort type = (ushort)value;
            byte[] typeBytes = BitConverter.GetBytes(type);
            Array.Reverse(typeBytes);
            Array.Copy(typeBytes, 0, Header.Data, Header.Offset + EtherFields.TypePosition, typeBytes.Length);
        }
    }

    public EtherPacket(ByteSegment data) : base(data) {
        Header.SegmentLength = EtherFields.HeaderLength;
        Payload = ParseNextPayload();
    }


    public override Payload ParseNextPayload() {
        ByteSegment nextSegment = Header.GetNextSegment();
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

