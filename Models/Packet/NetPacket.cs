using Models.Unit;
using SharpPcap;

namespace Models.Packet;

internal abstract class NetPacket(ByteSegment packet) {

    public ByteSegment Header { get; } = packet;

    protected Payload? Payload { get; set; } = null;

    public NetPacket? PayloadPacket => Payload?.Packet;

    public ByteSegment? PayloadBytes => Payload?.Bytes;

    protected virtual Payload? ParsePayload() {
        return null;
    }

    /// <summary>
    /// 解析包入口方法，从链路层开始解析
    /// </summary>
    /// <param name="rawCapture"><see cref="RawCapture"/>></param>
    /// <returns>实际返回<see cref="EtherPacket"/></returns>
    /// <exception cref="ArgumentException">LinkLayerType is not Ethernet.</exception>
    public static NetPacket ParsePacket(RawCapture rawCapture) {
        ByteSegment packet = new(rawCapture.Data);
        if (rawCapture.LinkLayerType != PacketDotNet.LinkLayers.Ethernet) {
            throw new ArgumentException("LinkLayerType is not Ethernet.");
        }
        return new EtherPacket(packet);
    }

}

