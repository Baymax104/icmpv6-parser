using System.Net;
using Models.Field;

namespace Models.Util;

public static class ByteSegmentExtensions {

    /// <summary>
    ///     将segment的position位置后length长度的字节流转换为ushort
    /// </summary>
    public static ushort ToUInt16(this ByteSegment segment, int position, int length = 2) {
        var bytes = new byte[length];
        Array.Copy(segment.Data, segment.Offset + position, bytes, 0, length);
        Array.Reverse(bytes);
        return BitConverter.ToUInt16(bytes, 0);
    }

    /// <summary>
    ///     将segment的position位置后length长度的字节流转换为uint
    /// </summary>
    public static uint ToUInt32(this ByteSegment segment, int position, int length = 4) {
        var bytes = new byte[length];
        Array.Copy(segment.Data, segment.Offset + position, bytes, 0, length);
        Array.Reverse(bytes);
        return BitConverter.ToUInt32(bytes, 0);
    }

    /// <summary>
    ///     将segment的position位置后length长度的字节流转换为IPv6地址
    /// </summary>
    public static IPAddress ToIp6Address(this ByteSegment segment, int position, int length = Ipv6Field.AddressLength) {
        var span = segment.Data.AsSpan(segment.Offset + position, length);
        return new(span);
    }

    public static MacAddress ToMacAddress(this ByteSegment segment, int position, int length = EtherField.MacAddressLength) {
        var array = new byte[length];
        var start = segment.Offset + position;
        Array.Copy(segment.Data, start, array, 0, length);
        return new(array);
    }
}
