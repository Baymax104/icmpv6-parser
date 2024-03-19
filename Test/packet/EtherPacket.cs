using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Test.constant;

namespace Test.model;
internal class EtherPacket : NetPacket {

    /// <summary>
    /// 源MAC地址
    /// </summary>
    public MacAddress SourceMacAddress {
        get {
            // 地址长度6B
            byte[] array = new byte[EtherFields.MacAddressLength];
            int start = Header.Offset + EtherFields.SourceMacPosition;
            Unsafe.CopyBlockUnaligned(ref array[0], ref Header.Data[start], (uint)EtherFields.MacAddressLength);
            return new(array);
        }
        set {
            byte[] bytes = value.GetAddressBytes();
            if (bytes.Length != EtherFields.MacAddressLength) {
                throw new ArgumentOutOfRangeException(nameof(value));
            }
            int start = Header.Offset + EtherFields.SourceMacPosition;
            Unsafe.CopyBlockUnaligned(ref Header.Data[start], ref bytes[0], (uint)EtherFields.MacAddressLength);
        }
    }

    /// <summary>
    /// 目的MAC地址
    /// </summary>
    public MacAddress DestinationMacAddress {
        get {
            byte[] array = new byte[EtherFields.MacAddressLength];
            int start = Header.Offset + EtherFields.DestinationMacPosition;
            Unsafe.CopyBlockUnaligned(ref array[0], ref Header.Data[start], (uint)EtherFields.MacAddressLength);
            return new(array);
        }
        set {
            byte[] bytes = value.GetAddressBytes();
            if (bytes.Length != EtherFields.MacAddressLength) {
                throw new ArgumentOutOfRangeException(nameof(value));
            }
            int start = Header.Offset + EtherFields.DestinationMacPosition;
            Unsafe.CopyBlockUnaligned(ref Header.Data[start], ref bytes[0], (uint)EtherFields.MacAddressLength);
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
        PayloadPacket = new(ParseNextPacket);
    }

    public EtherPacket(byte[] data) : base(new ByteSegment(data)) {
        Header.SegmentLength = EtherFields.HeaderLength;
        PayloadPacket = new(ParseNextPacket);
    }



    public override NetPacket ParseNextPacket() {
        ByteSegment nextSegment = Header.GetNextSegment();
        return Type switch {
            EtherType.IPv6 => new Ipv6Packet(nextSegment),
            _ => throw new NotSupportedException(nameof(Type)),
        };
    }
}

