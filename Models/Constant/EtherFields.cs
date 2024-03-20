using System.ComponentModel;

namespace Models.Constant;

public readonly struct EtherFields {

    /// <summary>
    /// 目的地址偏移
    /// </summary>
    public static readonly int DestinationMacPosition;

    /// <summary>
    /// 首部长度
    /// </summary>
    public static readonly int HeaderLength;

    /// <summary>
    /// MAC地址长度
    /// </summary>
    public static readonly int MacAddressLength;

    /// <summary>
    /// 源地址偏移
    /// </summary>
    public static readonly int SourceMacPosition;

    /// <summary>
    /// 类型字段长度
    /// </summary>
    public static readonly int TypeLength;

    /// <summary>
    /// 类型字段偏移
    /// </summary>
    public static readonly int TypePosition;


    static EtherFields() {
        DestinationMacPosition = 0;
        MacAddressLength = 6;
        TypeLength = 2;
        SourceMacPosition = MacAddressLength;
        TypePosition = MacAddressLength * 2;
        HeaderLength = TypePosition + TypeLength;
    }
}

internal enum EtherType {
    [Description("None (0x0)")]
    None = 0,

    [Description("IPv4 (0x0800)")]
    IPv4 = 0x0800,

    [Description("IPv6 (0x86DD)")]
    IPv6 = 0x86dd,
}

