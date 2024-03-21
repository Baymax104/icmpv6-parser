namespace Models.Unit;

public static class ByteSegmentExtensions {
    
    public static Span<byte> AsSpan(this ByteSegment segment, int offset, int length) {
        return segment.Data.AsSpan(segment.Offset + offset, length);
    }
}
