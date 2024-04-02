using SharpPcap;
using SharpPcap.LibPcap;

namespace Icmpv6.Repo;

public class Repository {

    public List<LibPcapLiveDevice> GetAllDevices() {
        return LibPcapLiveDeviceList.Instance.ToList();
    }

    public Task<RawCapture?> Capture(LibPcapLiveDevice device, CancellationToken token) {
        device.Open(DeviceModes.Promiscuous);
        device.Filter = "ip6";
        return Task.Run(
            () => {
                var status = device.GetNextPacket(out var packetCapture);
                return status is GetPacketStatus.PacketRead ? packetCapture.GetPacket() : null;
            }, token);
    }
}
