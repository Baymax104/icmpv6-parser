using System.Collections;
using System.Reflection;
using Models.Packet;
using Models.Packet.Icmp6;
using Models.Packet.Icmp6.Ndp;

namespace Icmpv6.VO;

public record PacketAttributeView : IEnumerable<AttributeItem> {

    public List<AttributeItem> Attributes { get; init; }

    public NetPacket Instance { get; init; }

    public string PacketName {
        get {
            return Instance switch {
                EtherPacket => "Ethernet II",
                Ip6Packet => "Internet Protocol Version 6 (IPv6)",
                Icmp6Packet => "Internet Control Message Protocol v6 (ICMPv6)",
                EchoReplyPacket => "Echo Reply",
                EchoRequestPacket => "Echo Request",
                NeighborAdvertisementPacket => "Neighbor Advertisement (NA)",
                NeighborSolicitationPacket => "Neighbor Solicitation (NS)",
                RouterAdvertisementPacket => "Router Advertisement (RA)",
                RouterSolicitationPacket => "Router Solicitation (RS)",
                DestinationUnreachablePacket => "Destination Unreachable",
                PacketTooBigPacket => "Packet Too Big",
                ParameterProblemPacket => "Parameter Problem",
                TimeExceededPacket => "Time Exceeded",
                UdpPacket => "User Datagram Protocol (UDP)",
                _ => "Unknown"
            };
        }
    }

    public PacketAttributeView(NetPacket instance) {
        Instance = instance;
        Attributes = [];
        var type = instance.GetType();
        var properties = type.GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance);
        foreach (var property in properties) {
            var name = property.Name;
            var value = property.GetValue(instance)?.ToString() ?? string.Empty;
            Attributes.Add(new(name, value));
        }
    }

    public IEnumerator<AttributeItem> GetEnumerator() {
        return Attributes.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator() {
        return GetEnumerator();
    }
}
