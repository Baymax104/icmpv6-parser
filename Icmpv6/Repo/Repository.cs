using SharpPcap.LibPcap;

namespace Icmpv6.Repo;

public class Repository {

    public List<LibPcapLiveDevice> GetAllDevices() {
        return LibPcapLiveDeviceList.Instance.ToList();
    }
}
