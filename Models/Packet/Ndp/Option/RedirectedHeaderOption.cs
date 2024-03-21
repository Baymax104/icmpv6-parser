﻿using Models.Field;
using Models.Unit;

namespace Models.Packet.Ndp.Option;

internal class RedirectedHeaderOption(ByteSegment data) : NdpOption(data) {

    public byte[] IpHeaderData {
        get {
            var start = Payload.Offset + RedirectedHeaderOptionField.IpHeaderDataPosition;
            var IpHeaderDataLength = Header.Offset + Length * 8 - start;
            var bytes = new byte[IpHeaderDataLength];
            Array.Copy(Payload.Data, start, bytes, 0, IpHeaderDataLength);
            return bytes;
        }
        set {
            var start = Payload.Offset + RedirectedHeaderOptionField.IpHeaderDataPosition;
            var IpHeaderDataLength = Header.Offset + Length * 8 - start;
            Array.Copy(value, 0, Payload.Data, start, IpHeaderDataLength);
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
