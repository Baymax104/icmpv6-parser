using System.ComponentModel;

namespace Models.Type;

public enum Icmp6Type : byte {
    [Description("目的地不可达")]
    DestinationUnreachable = 1,

    [Description("包过大")]
    PacketTooBig = 2,

    [Description("超时")]
    TimeExceeded = 3,

    [Description("参数错误")]
    ParameterProblem = 4,

    [Description("回显请求")]
    EchoRequest = 128,

    [Description("回显应答")]
    EchoReply = 129,

    [Description("路由请求")]
    RouterSolicitation = 133,

    [Description("路由通告")]
    RouterAdvertisement = 134,

    [Description("邻居请求")]
    NeighborSolicitation = 135,

    [Description("邻居通告")]
    NeighborAdvertisement = 136,

    [Description("重定向")]
    RedirectMessage = 137
}
