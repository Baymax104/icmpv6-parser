using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Constant;
internal enum ProtocolType : byte {
    IPv6HopByHopOptions = 0,
    Icmp = 1,
    Igmp = 2,
    Gpg = 3,
    IPv4 = 4,
    Tcp = 6,
    Egp = 8,
    Pup = 12,
    Udp = 17,
    Idp = 22,
    TP = 29,
    IPv6 = 41,
    IPv6RoutingHeader = 43,
    IPv6FragmentHeader = 44,
    Rsvp = 46,
    Gre = 47,
    IPSecEncapsulatingSecurityPayload = 50,
    IPSecAuthenticationHeader = 51,
    IcmpV6 = 58,
    IPv6NoNextHeader = 59,
    IPv6DestinationOptions = 60,
}

