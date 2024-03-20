using Models.Field;
using Models.Unit;

namespace Models.Packet.Ndp.Option;

internal class MtuOption(ByteSegment data) : NdpOption(data) {

    public uint Mtu {
        get {
            byte[] bytes = new byte[MtuOptionField.MtuLength];
            int start = Payload.Offset + MtuOptionField.MtuPosition;
            Array.Copy(Payload.Data, start, bytes, 0, MtuOptionField.MtuLength);
            Array.Reverse(bytes);
            return BitConverter.ToUInt32(bytes, 0);
        }
        set {
            byte[] bytes = BitConverter.GetBytes(value);
            Array.Reverse(bytes);
            int start = Payload.Offset + MtuOptionField.MtuPosition;
            Array.Copy(bytes, 0, Payload.Data, start, MtuOptionField.MtuLength);
        }
    }
}
