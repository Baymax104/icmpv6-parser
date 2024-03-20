using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Constant;

public readonly struct Icmp6Fields {

    /// <summary>
    /// 校验和长度
    /// </summary>
    public static readonly int ChecksumLength;

    /// <summary>
    /// 校验和偏移
    /// </summary>
    public static readonly int ChecksumPosition;

    /// <summary>
    /// Code长度
    /// </summary>
    public static readonly int CodeLength;

    /// <summary>
    /// Code偏移
    /// </summary>
    public static readonly int CodePosition;

    /// <summary>
    /// 消息类型长度
    /// </summary>
    public static readonly int TypeLength;

    /// <summary>
    /// 消息类型偏移
    /// </summary>
    public static readonly int TypePosition;

    /// <summary>
    /// 消息偏移
    /// </summary>
    public static readonly int MessagePosition;

    /// <summary>
    /// 首部长度
    /// </summary>
    public static readonly int HeaderLength;

    static Icmp6Fields() {
        ChecksumLength = 2;
        CodeLength = 1;
        TypeLength = 1;
        TypePosition = 0;
        CodePosition = TypePosition + TypeLength;
        ChecksumPosition = CodePosition + CodeLength;
        MessagePosition = ChecksumPosition + ChecksumLength;
        HeaderLength = ChecksumPosition + ChecksumLength;
    }
}

public enum IcmpV6Type : byte {
    [Description("目的地不可达")]
    DestinationUnreachable = 1,

    [Description("包过大")]
    PacketTooBig = 2,

    [Description("超时")]
    TimeExceeded = 3,

    [Description("参数错误")]
    ParameterProblem = 4,

    [Description("差错报文扩展")]
    ReservedForExpansion1 = 127,

    [Description("回显请求")]
    EchoRequest = 128,

    [Description("回显应答")]
    EchoReply = 129,

    MulticastListenerQuery = 130,
    MulticastListenerReport = 131,
    MulticastListenerDone = 132,

    [Description("路由请求")]
    RouterSolicitation = 133,

    [Description("路由通告")]
    RouterAdvertisement = 134,

    [Description("邻居请求")]
    NeighborSolicitation = 135,

    [Description("邻居通告")]
    NeighborAdvertisement = 136,

    [Description("重定向")]
    RedirectMessage = 137,

    RouterRenumbering = 138,
    ICMPNodeInformationQuery = 139,
    ICMPNodeInformationResponse = 140,
    InverseNeighborDiscoverySolicitationMessage = 141,
    InverseNeighborDiscoveryAdvertisementMessage = 142,
    Version2MulticastListenerReport = 143,
    HomeAgentAddressDiscoveryRequestMessage = 144,
    HomeAgentAddressDiscoveryReplyMessage = 145,
    MobilePrefixSolicitation = 146,
    MobilePrefixAdvertisement = 147,
    CertificationPathSolicitationMessage = 148,
    CertificationPathAdvertisementMessage = 149,
    ExperimentalMobilityProtocols = 150,
    MulticastRouterAdvertisement = 151,
    MulticastRouterSolicitation = 152,
    MulticastRouterTermination = 153,
    FmIPv6Messages = 154,
    RplControlMessage = 155,
    IlnpV6LocatorUpdateMessage = 156,
    DuplicateAddressRequest = 157,
    DuplicateAddressConfirmation = 158,
    MplControlMessage = 159,
    ExtendedEchoRequest = 160,
    ExtendedEchoReply = 161,

    [Description("消息报文扩展")]
    ReservedForExpansion2 = byte.MaxValue
}
