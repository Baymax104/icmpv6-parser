using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.Constant;
using Models.Unit;

namespace Models.Packet;
internal class Icmp6Packet : NetPacket {

    public IcmpV6Type Type {
        get => (IcmpV6Type)Header[Icmp6Fields.TypePosition];
        set {
            Header[Icmp6Fields.TypePosition] = (byte)value;
        }
    }

    public int Code {
        get => Header[Icmp6Fields.CodePosition];
        set {
            Header[Icmp6Fields.CodePosition] = (byte)value;
        }
    }

    public ushort Checksum {
        get {
            byte[] bytes = new byte[Icmp6Fields.ChecksumLength];
            int start = Header.Offset + Icmp6Fields.ChecksumPosition;
            Array.Copy(Header.Data, start, bytes, 0, Icmp6Fields.ChecksumLength);
            Array.Reverse(bytes);
            return (ushort)BitConverter.ToInt16(bytes, 0);
        }
        set {
            byte[] bytes = BitConverter.GetBytes(value);
            Array.Reverse(bytes);
            int start = Header.Offset + Icmp6Fields.ChecksumPosition;
            Array.Copy(bytes, 0, Header.Data, start, Icmp6Fields.ChecksumLength);
        }
    }
    
    public Icmp6Packet(ByteSegment data) : base(data) {
        Header.SegmentLength = Icmp6Fields.HeaderLength;
    }

    public override Payload ParseNextPayload() {
        throw new NotImplementedException();
    }

    public override string ToString() => $@"
{{
    {nameof(Type)} = {Type},
    {nameof(Code)} = {Code},
    {nameof(Checksum)} = {Checksum:x}
}}    
    ".Trim();
}

