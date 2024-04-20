using System.ComponentModel;

namespace Models.Type;

public enum NdpOptionType : byte {
    [Description("Source LinkLayer Address (1)")]
    SourceLinkLayerAddress = 1,
    
    [Description("Target LinkLayer Address (2)")]
    TargetLinkLayerAddress = 2,
    
    [Description("Prefix Information (3)")]
    PrefixInformation = 3,
    
    [Description("Redirected Header (4)")]
    RedirectedHeader = 4,
    
    [Description("MTU (5)")]
    Mtu = 5
}
