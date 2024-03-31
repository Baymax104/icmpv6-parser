namespace Icmpv6.VO;

public record InfoView {

    public enum InfoViewType {
        Device,
        Packet,
        None
    }

    private DeviceView? device;

    private string? packet;

    public InfoView() {
    }

    public InfoView(DeviceView device) {
        this.device = device;
    }

    public InfoView(string packet) {
        this.packet = packet;
    }


    public DeviceView Device {
        get => device ?? new DeviceView();
        set {
            packet = null;
            device = value;
        }
    }

    public string Packet {
        get => packet ?? "";
    }

    public InfoViewType Type {
        get {
            if (device != null) {
                return InfoViewType.Device;
            }
            if (packet != null) {
                return InfoViewType.Packet;
            }
            return InfoViewType.None;
        }
    }
}
