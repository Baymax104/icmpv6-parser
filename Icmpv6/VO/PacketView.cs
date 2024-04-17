using Models.Packet;
using NdpPacket = Models.Packet.Icmp6.Ndp.NdpPacket;

namespace Icmpv6.VO;

public record PacketView {
    
    public List<PacketAttributeView> Packets { get; }
    
    public NetPacket Instance { get; init; }
    
    public int Id { get; init; }

    public PacketView(NetPacket instance) {
        Instance = instance;
        Packets = [];
        var packets = NetPacket.FlatExtract(instance);
        foreach (var packet in packets) {
            Packets.Add(new(packet));
            if (packet is NdpPacket ndpPacket) {
                var options = ndpPacket.Options;
                foreach (var option in options) {
                    Packets.Add(new(option));
                }
            }
        }
    }
}
