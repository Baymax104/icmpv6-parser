namespace Icmpv6.VO;

public class InfoView {

    public enum InfoViewType {
        Device,
        Packet,
        None
    }

    private DeviceView? device;
    public DeviceView Device {
        get => device ?? new DeviceView("", "", "", "");
        set {
            packet = null;
            device = value;
        }
    }

    private string? packet;
    public string Packet {
        get => packet ?? "";
        set {
            device = null;
            packet = value;
        }
    }

    public InfoViewType Type {
        get {
            if (device != null) {
                return InfoViewType.Device;
            } else if (packet != null) {
                return InfoViewType.Packet;
            } else {
                return InfoViewType.None;
            }
        }
    }


    public InfoView() {
    }

    public InfoView(DeviceView device) {
        this.device = device;
    }

    public InfoView(string packet) {
        this.packet = packet;
    }
}
