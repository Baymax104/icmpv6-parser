using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.model;

internal class ByteSegment(byte[] data, int offset, int segmentLength) : IEnumerable<byte> {

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
    /// 字节数组
    /// </summary>
    public byte[] Data { get; } = data;


    public ByteSegment(byte[] data) : this(data, 0, 0) { }

    public ByteSegment(byte[] data, int offset) : this(data, offset, 0) { }

    public ByteSegment GetNextSegment() {
        int length = Data.Length - (offset + segmentLength);
        return GetNextSegment(length);
    }

    public ByteSegment GetNextSegment(int length) {
        int startOffset = offset + segmentLength;
        length = Math.Min(length, Data.Length - startOffset);
        return new ByteSegment(Data, startOffset, length);
    }

    public IEnumerator<byte> GetEnumerator() {
        int to = offset + segmentLength;
        for (int i = Offset; i < to; i++) {
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

