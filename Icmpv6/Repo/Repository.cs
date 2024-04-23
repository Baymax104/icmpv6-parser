using SharpPcap;
using SharpPcap.LibPcap;

namespace Icmpv6.Repo;

public class Repository {

    public IEnumerable<LibPcapLiveDevice> GetAllDevices() {
        var devices = LibPcapLiveDeviceList.Instance.ToList()
            .Where(d => !d.Loopback)
            .Where(d => d.MacAddress != null && d.MacAddress.GetAddressBytes().Length != 0);
        return devices;
    }

    public Task<RawCapture?> CaptureAsync(LibPcapLiveDevice device, CancellationToken token) {
        return Task.Run(
            () => {
                var status = device.GetNextPacket(out var packetCapture);
                return status is GetPacketStatus.PacketRead ? packetCapture.GetPacket() : null;
            }, token);
    }

    public void SaveFile(string Filename, IEnumerable<RawCapture> rawCaptures) {
        using var writer = new CaptureFileWriterDevice(Filename);
        writer.Open();
        foreach (var item in rawCaptures) {
            writer.Write(item);
        }
    }

    public IEnumerable<RawCapture> OpenFile(string Filename) {
        using var reader = new CaptureFileReaderDevice(Filename);
        reader.Open();
        var result = new List<RawCapture>();
        while (reader.GetNextPacket(out var packetCapture) == GetPacketStatus.PacketRead) {
            result.Add(packetCapture.GetPacket());
        }
        return result;
    }
}
