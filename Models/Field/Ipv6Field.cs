namespace Models.Field;

public readonly struct Ipv6Field {

    /// <summary>
    ///     版本(4bit)+流量类型(8bit)+流标签(20bit) = 32bit = 4B
    /// </summary>
    public const int VersionTrafficClassFlowLabelLength = 4;

    /// <summary>
    ///     分片扩展头的FragmentOffset偏移
    /// </summary>
    public const int FragmentOffsetPosition = 2;

    /// <summary>
    ///     PayloadLength长度
    /// </summary>
    public const int PayloadLengthLength = 2;

    /// <summary>
    ///     NextHeader长度
    /// </summary>
    public const int NextHeaderLength = 1;

    /// <summary>
    ///     HopLimit长度
    /// </summary>
    public const int HopLimitLength = 1;

    /// <summary>
    ///     地址长度
    /// </summary>
    public const int AddressLength = 16;

    /// <summary>
    ///     版本+流量类型+流标签偏移
    /// </summary>
    public const int VersionTrafficClassFlowLabelPosition = 0;

    /// <summary>
    ///     PayloadLength偏移
    /// </summary>
    public const int PayloadLengthPosition = 4;

    /// <summary>
    ///     NextHeader偏移
    /// </summary>
    public const int NextHeaderPosition = 6;

    /// <summary>
    ///     HopLimit偏移
    /// </summary>
    public const int HopLimitPosition = 7;

    /// <summary>
    ///     源地址偏移
    /// </summary>
    public const int SourceAddressPosition = 8;

    /// <summary>
    ///     目的地址偏移
    /// </summary>
    public const int DestinationAddressPosition = 24;

    /// <summary>
    ///     首部长度
    /// </summary>
    public const int HeaderLength = 40;

    /// <summary>
    ///     Teredo端口
    /// </summary>
    public const int TeredoPort = 3544;
}
