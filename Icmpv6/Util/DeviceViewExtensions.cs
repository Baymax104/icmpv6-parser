using System.Collections;
using Icmpv6.VO;
using SharpPcap.LibPcap;

namespace Icmpv6.Util;

public static class DeviceViewExtensions {
    
    public static DeviceView ToView(this LibPcapLiveDevice device) {
        var _interface = device.Interface;
        var table = new Hashtable();
        
        table["name"] = _interface.FriendlyName;
        foreach (var address in _interface.Addresses) {
            if (address.Addr.type == Sockaddr.AddressTypes.AF_INET_AF_INET6) {
                var ipAddress = address.Addr.ipAddress;
                
            } else if (address.Addr.type == Sockaddr.AddressTypes.HARDWARE) {
                
            }
        }
        return null;
    }
}
