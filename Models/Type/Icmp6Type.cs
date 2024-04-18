using System.ComponentModel;

namespace Models.Type;

public enum Icmp6Type : byte {
    [Description("DestinationUnreachable (1)")]
    DestinationUnreachable = 1,

    [Description("PacketTooBig (2)")]
    PacketTooBig = 2,

    [Description("TimeExceeded (3)")]
    TimeExceeded = 3,

    [Description("ParameterProblem (4)")]
    ParameterProblem = 4,

    [Description("EchoRequest (128)")]
    EchoRequest = 128,

    [Description("EchoReply (129)")]
    EchoReply = 129,

    [Description("RouterSolicitation (133)")]
    RouterSolicitation = 133,

    [Description("RouterAdvertisement (134)")]
    RouterAdvertisement = 134,

    [Description("NeighborSolicitation (135)")]
    NeighborSolicitation = 135,

    [Description("NeighborAdvertisement (136)")]
    NeighborAdvertisement = 136,

    [Description("RedirectMessage (137)")]
    RedirectMessage = 137
}
