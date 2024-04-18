using System.ComponentModel;

namespace Models.Type;

public enum IpVersion {
    [Description("IPv4 (4)")]
    IPv4 = 4,
    [Description("IPv6 (6)")]
    IPv6 = 6
}
