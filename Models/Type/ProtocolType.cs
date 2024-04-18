using System.ComponentModel;

namespace Models.Type;

public enum ProtocolType : byte {
    IPv6HopByHopOptions = 0,
    Icmp = 1,
    Igmp = 2,
    Ipv4 = 4,
    Tcp = 6,
    Egp = 8,
    [Description("UDP (17)")]
    Udp = 17,
    Ipv6 = 41,
    Ipv6RoutingHeader = 43,
    Ipv6FragmentHeader = 44,
    [Description("ICMPv6 (58)")]
    Icmpv6 = 58,
    Ipv6NoNextHeader = 59,
    Ipv6DestinationOptions = 60
}
