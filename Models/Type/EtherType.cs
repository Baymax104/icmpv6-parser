using System.ComponentModel;

namespace Models.Type;

public enum EtherType {
    [Description("None (0x0)")]
    None = 0,

    [Description("IPv4 (0x0800)")]
    IPv4 = 0x0800,

    [Description("IPv6 (0x86DD)")]
    IPv6 = 0x86dd
}
