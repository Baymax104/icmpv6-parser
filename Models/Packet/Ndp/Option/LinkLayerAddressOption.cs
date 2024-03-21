using Models.Field;
using Models.Unit;

namespace Models.Packet.Ndp.Option;

internal class LinkLayerAddressOption(ByteSegment data) : NdpOption(data) {

    public MacAddress LinkLayerAddress {
        get {
            var bytes = new byte[EtherField.MacAddressLength];
            Array.Copy(Payload.Data, Payload.Offset, bytes, 0, EtherField.MacAddressLength);
            return new(bytes);
        }
        set {
            var address = value.GetAddressBytes();
            if (address.Length != EtherField.MacAddressLength) {
                throw new ArgumentException(nameof(address));
            }
            Array.Copy(address, 0, Payload.Data, Payload.Offset, EtherField.MacAddressLength);
        }
    }

    public override string ToString() {
        return $@"
    {{
        {nameof(Type)} = {Type},
        {nameof(Length)} = {Length},
        {nameof(LinkLayerAddress)} = {LinkLayerAddress}
    }}";
    }
}
