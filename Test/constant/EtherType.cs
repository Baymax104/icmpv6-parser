using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Test.constant;
internal enum EtherType {
    [Description("None (0x0)")]
    None = 0,

    [Description("IPv4 (0x0800)")]
    IPv4 = 0x0800,

    [Description("IPv6 (0x86DD)")]
    IPv6 = 0x86dd,
}

