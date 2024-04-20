using System.ComponentModel;

namespace Models.Type;

public enum Icmp6Type : byte {
    [Description("Destination Unreachable (1)")]
    DestinationUnreachable = 1,

    [Description("Packet Too Big (2)")]
    PacketTooBig = 2,

    [Description("Time Exceeded (3)")]
    TimeExceeded = 3,

    [Description("Parameter Problem (4)")]
    ParameterProblem = 4,

    [Description("Echo Request (128)")]
    EchoRequest = 128,

    [Description("Echo Reply (129)")]
    EchoReply = 129,

    [Description("Router Solicitation (133)")]
    RouterSolicitation = 133,

    [Description("Router Advertisement (134)")]
    RouterAdvertisement = 134,

    [Description("Neighbor Solicitation (135)")]
    NeighborSolicitation = 135,

    [Description("Neighbor Advertisement (136)")]
    NeighborAdvertisement = 136,

    [Description("Redirect Message (137)")]
    RedirectMessage = 137
}
