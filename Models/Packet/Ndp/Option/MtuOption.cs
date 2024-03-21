using Models.Field;
using Models.Unit;

namespace Models.Packet.Ndp.Option;

internal class MtuOption(ByteSegment data) : NdpOption(data) {

    public uint Mtu {
        get {
            var bytes = new byte[MtuOptionField.MtuLength];
            var start = Payload.Offset + MtuOptionField.MtuPosition;
            Array.Copy(Payload.Data, start, bytes, 0, MtuOptionField.MtuLength);
            Array.Reverse(bytes);
            return BitConverter.ToUInt32(bytes, 0);
        }
        set {
            var bytes = BitConverter.GetBytes(value);
            Array.Reverse(bytes);
            var start = Payload.Offset + MtuOptionField.MtuPosition;
            Array.Copy(bytes, 0, Payload.Data, start, MtuOptionField.MtuLength);
        }
    }

    public override string ToString() {
        return $@"
    {{
        {nameof(Type)} = {Type},
        {nameof(Length)} = {Length},
        {nameof(Mtu)} = {Mtu}
    }}";
    }
}
