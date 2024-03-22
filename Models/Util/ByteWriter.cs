using System.Net;

namespace Models.Util;

public static class ByteWriter {

    public static void WriteTo(ByteSegment segment, ushort value, int position, int length = 2) {
        var bytes = BitConverter.GetBytes(value);
        Array.Reverse(bytes);
        Array.Copy(bytes, 0, segment.Data, segment.Offset + position, length);
    }

    public static void WriteTo(ByteSegment segment, uint value, int position, int length = 4) {
        var bytes = BitConverter.GetBytes(value);
        Array.Reverse(bytes);
        Array.Copy(bytes, 0, segment.Data, segment.Offset + position, length);
    }

    public static void WriteTo(ByteSegment segment, IPAddress value, int position) {
        var bytes = value.GetAddressBytes();
        Array.Copy(bytes, 0, segment.Data, segment.Offset + position, bytes.Length);
    }

    public static void WriteTo(ByteSegment segment, MacAddress value, int position) {
        var bytes = value.GetAddressBytes();
        var start = segment.Offset + position;
        Array.Copy(bytes, 0, segment.Data, start, bytes.Length);
    }
}
