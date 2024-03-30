using System.Net;
using SharpPcap.LibPcap;

namespace Icmpv6.VO;

public class DeviceView {

    public record AttributeItem(string Property, string Value);

    public string Name { get; init; } = "";

    public string Description { get; init; } = "";

    public string MacAddress { get; set; } = "";

    public List<string> GatewayAddress { get; set; } = [];

    public List<AddressView> Addresses { get; set; } = [];

    public List<AttributeItem> Attributes {
        get {
            var attributes = new List<AttributeItem> {
                new("设备名称", Name),
                new("设备描述", Description),
                new("MAC地址", MacAddress)
            };
            for (var i = 0; i < GatewayAddress.Count; i++) {
                attributes.Add(new($"默认网关{i + 1}", GatewayAddress[i]));
            }
            return attributes;
        }
    }

    public DeviceView() {
    }

    public DeviceView(string name) {
        Name = name;
    }
}
