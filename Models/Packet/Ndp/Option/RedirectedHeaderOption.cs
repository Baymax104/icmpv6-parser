using Models.Field;
using Models.Unit;

namespace Models.Packet.Ndp.Option;
internal class RedirectedHeaderOption(ByteSegment data) : NdpOption(data) {

    public byte[] IpHeaderData {
        get {
            int start = Payload.Offset + RedirectedHeaderOptionField.IpHeaderDataPosition;
            int IpHeaderDataLength = Header.Offset + Length * 8 - start;
            byte[] bytes = new byte[IpHeaderDataLength];
            Array.Copy(Payload.Data, start, bytes, 0, IpHeaderDataLength);
            return bytes;
        }
        set {
            int start = Payload.Offset + RedirectedHeaderOptionField.IpHeaderDataPosition;
            int IpHeaderDataLength = Header.Offset + Length * 8 - start;
            Array.Copy(value, 0, Payload.Data, start, IpHeaderDataLength);
        }
    }

}

