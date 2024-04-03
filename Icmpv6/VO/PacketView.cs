using Models.Packet;

namespace Icmpv6.VO;

public record PacketView {
    
    public IEnumerable<PacketAttributeView> Packets { get; init; }
    
    public NetPacket Instance { get; init; }
    
    public int Id { get; init; }

    public PacketView(NetPacket instance) {
        Instance = instance;
        var packets = NetPacket.ExtractAll(instance);
        Packets = packets.Select(p => new PacketAttributeView(p));
    }
}
