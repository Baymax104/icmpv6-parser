using System.Net.Sockets;
using Models.Util;
using SharpPcap.LibPcap;

namespace Icmpv6.VO;

public static class DeviceViewExtensions {

    public static DeviceView ToView(this LibPcapLiveDevice device) {
        var inter = device.Interface;
        var view = new DeviceView {
            Name = !string.IsNullOrEmpty(inter.FriendlyName) ? inter.FriendlyName : inter.Name,
            Description = !string.IsNullOrEmpty(inter.Description) ? inter.Description : "无"
        };

        inter.GatewayAddresses.ForEach(address => view.GatewayAddress.Add(address.ToString()));
        foreach (var a in inter.Addresses) {

            if (a == null) {
                continue;
            }
            var address = a.Addr;
            var netmask = a.Netmask;
            var broadcast = a.Broadaddr;


            // 设置MAC
            if (address.type == Sockaddr.AddressTypes.HARDWARE) {
                view.MacAddress = new MacAddress(address.hardwareAddress).ToString();
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
                view.Addresses.Add(addressView);
            }
        }
        return view;
    }
}
