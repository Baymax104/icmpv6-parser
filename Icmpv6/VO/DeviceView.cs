using System.Net.Sockets;
using Models.Util;
using SharpPcap.LibPcap;

namespace Icmpv6.VO;

public record DeviceView {

    public DeviceView() {
    }

    public DeviceView(LibPcapLiveDevice instance) {
        this.instance = instance;
        var inter = instance.Interface;
        Name = !string.IsNullOrEmpty(inter.FriendlyName) ? inter.FriendlyName : inter.Name;
        Description = !string.IsNullOrEmpty(inter.Description) ? inter.Description : "无";

        inter.GatewayAddresses.ForEach(address => GatewayAddress.Add(address.ToString()));
        foreach (var a in inter.Addresses) {

            if (a == null) {
                continue;
            }
            var address = a.Addr;
            var netmask = a.Netmask;
            var broadcast = a.Broadaddr;

            // 设置MAC
            if (address.type == Sockaddr.AddressTypes.HARDWARE) {
                MacAddress = new MacAddress(address.hardwareAddress).ToString();
                continue;
            }

            // 添加地址项
            if (address.type == Sockaddr.AddressTypes.AF_INET_AF_INET6) {
                var addressView = new AddressView {
                    Address = address.ipAddress?.ToString() ?? "",
                    Netmask = netmask.ipAddress?.ToString() ?? "",
                    Broadcast = broadcast.ipAddress?.ToString() ?? "",
                    Type = address.ipAddress?.AddressFamily switch {
                        AddressFamily.InterNetwork => "IPv4",
                        AddressFamily.InterNetworkV6 => "IPv6",
                        _ => ""
                    }
                };
                Addresses.Add(addressView);
            }
        }
    }

    public string Name { get; set; } = "";

    public string Description { get; set; } = "";

    public string MacAddress { get; set; } = "";

    public List<string> GatewayAddress { get; set; } = [];

    public List<AddressView> Addresses { get; set; } = [];

    private readonly LibPcapLiveDevice? instance;
    public LibPcapLiveDevice Instance {
        get {
            if (instance == null) {
                throw new NotSupportedException("Instance is null.");
            }
            return instance;
        }
    }

    public bool HasInstance {
        get => instance != null;
    }

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
