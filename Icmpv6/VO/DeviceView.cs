namespace Icmpv6.VO;

public record DeviceView {

    public DeviceView() {
    }

    public DeviceView(string name) {
        Name = name;
    }

    public string Name { get; set; } = "";

    public string Description { get; set; } = "";

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

    public record AttributeItem(string Property, string Value);
}
