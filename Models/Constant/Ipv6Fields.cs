namespace Models.Constant;
public readonly struct Ipv6Fields {

    /// <summary>
    /// 版本(4bit)+流量类型(8bit)+流标签(20bit) = 32bit = 4B
    /// </summary>
    public static readonly int VersionTrafficClassFlowLabelLength;

    /// <summary>
    /// 分片扩展头的FragmentOffset偏移
    /// </summary>
    public static readonly int FragmentOffsetPosition;

    /// <summary>
    /// PayloadLength长度
    /// </summary>
    public static readonly int PayloadLengthLength;

    /// <summary>
    /// NextHeader长度
    /// </summary>
    public static readonly int NextHeaderLength;

    /// <summary>
    /// HopLimit长度
    /// </summary>
    public static readonly int HopLimitLength;

    /// <summary>
    /// 地址长度
    /// </summary>
    public static readonly int AddressLength;

    /// <summary>
    /// 版本+流量类型+流标签偏移
    /// </summary>
    public static readonly int VersionTrafficClassFlowLabelPosition;

    /// <summary>
    /// PayloadLength偏移
    /// </summary>
    public static readonly int PayloadLengthPosition;

    /// <summary>
    /// NextHeader偏移
    /// </summary>
    public static readonly int NextHeaderPosition;

    /// <summary>
    /// HopLimit偏移
    /// </summary>
    public static readonly int HopLimitPosition;

    /// <summary>
    /// 源地址偏移
    /// </summary>
    public static readonly int SourceAddressPosition;

    /// <summary>
    /// 目的地址偏移
    /// </summary>
    public static readonly int DestinationAddressPosition;

    /// <summary>
    /// 首部长度
    /// </summary>
    public static readonly int HeaderLength;

    /// <summary>
    /// Teredo端口
    /// </summary>
    public static readonly int TeredoPort;

    static Ipv6Fields() {
        VersionTrafficClassFlowLabelLength = 4;
        FragmentOffsetPosition = 2;
        PayloadLengthLength = 2;
        NextHeaderLength = 1;
        HopLimitLength = 1;
        AddressLength = 16;
        VersionTrafficClassFlowLabelPosition = 0;
        TeredoPort = 3544;
        PayloadLengthPosition = VersionTrafficClassFlowLabelPosition + VersionTrafficClassFlowLabelLength;
        NextHeaderPosition = PayloadLengthPosition + PayloadLengthLength;
        HopLimitPosition = NextHeaderPosition + NextHeaderLength;
        SourceAddressPosition = HopLimitPosition + HopLimitLength;
        DestinationAddressPosition = SourceAddressPosition + AddressLength;
        HeaderLength = DestinationAddressPosition + AddressLength;
    }
}