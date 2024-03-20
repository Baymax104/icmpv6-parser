using System.Collections;

namespace Models.Unit;

internal class ByteSegment(byte[] data, int offset, int segmentLength, int byteLength) : IEnumerable<byte> {

    /// <summary>
    /// 片段长度
    /// </summary>
    private int segmentLength = segmentLength;
    public int SegmentLength {
        get => segmentLength;
        set {
            if (value < 0) {
                value = 0;
            }
            segmentLength = value;
        }
    }

    /// <summary>
    /// 字节数组偏移
    /// </summary>
    private int offset = offset;
    public int Offset {
        get => offset;
        set {
            if (value < 0) {
                value = 0;
            }
            offset = value;
        }
    }

    /// <summary>
    /// 字节数组实际处理长度
    /// </summary>
    private int actualLength = Math.Min(byteLength, data.Length);
    public int ActualLength {
        get => actualLength;
        set {
            if (actualLength < 0) {
                value = 0;
            }
            actualLength = value;
        }
    }

    /// <summary>
    /// 字节数组
    /// </summary>
    public byte[] Data { get; } = data;


    public ByteSegment(byte[] data) : this(data, 0, 0, data.Length) { }


    public byte this[int index] {
        get => Data[offset + index];
        set {
            Data[offset + index] = value;
        }
    }

    public ByteSegment GetNextSegment() {
        int length = Data.Length - (offset + segmentLength);
        return GetNextSegment(length);
    }

    public ByteSegment GetNextSegment(int length) {
        int startOffset = offset + segmentLength;
        length = Math.Min(length, actualLength - startOffset);
        int bytesLength = startOffset + length;
        return new ByteSegment(Data, startOffset, length, bytesLength);
    }

    public IEnumerator<byte> GetEnumerator() {
        int length = offset + segmentLength;
        for (int i = offset; i < length; i++) {
            yield return Data[i];
        }
    }

    IEnumerator IEnumerable.GetEnumerator() {
        return GetEnumerator();
    }

    public override string ToString() {
        return $"{{{nameof(SegmentLength)}={SegmentLength}, {nameof(Offset)}={Offset}, {nameof(Data)}={Data}}}";
    }
}

