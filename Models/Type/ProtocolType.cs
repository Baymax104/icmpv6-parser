using System.ComponentModel;

namespace Models.Type;

public enum ProtocolType : byte {
    IPv6HopByHopOptions = 0,
    ICMP = 1,
    IGMP = 2,
    IPv4 = 4,
    TCP = 6,
    EGP = 8,
    [Description("UDP (17)")]
    UDP = 17,
    IPv6 = 41,
    Ipv6RoutingHeader = 43,
    Ipv6FragmentHeader = 44,
    [Description("ICMPv6 (58)")]
    ICMPv6 = 58,
    Ipv6NoNextHeader = 59,
    Ipv6DestinationOptions = 60
}
