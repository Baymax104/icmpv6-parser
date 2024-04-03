namespace Icmpv6.VO;

public record InfoView {

    public enum InfoType {
        Device,
        Packet,
        None
    }

    private DeviceView? device;

    private PacketView? packet;

    public InfoView() {
    }

    public InfoView(DeviceView device) {
        this.device = device;
    }

    public InfoView(PacketView packet) {
        this.packet = packet;
    }


    public DeviceView Device {
        get => device ?? throw new NotSupportedException("Device instance is null.");
        set {
            packet = null;
            device = value;
        }
    }

    public PacketView Packet {
        get => packet ?? throw new NotSupportedException("Packet instance is null.");
        set {
            device = null;
            packet = value;
        }
    }

    public InfoType Type {
        get {
            if (device != null) {
                return InfoType.Device;
            }
            if (packet != null) {
                return InfoType.Packet;
            }
            return InfoType.None;
        }
    }

    public override string ToString() {
        return $"InfoView({nameof(Device)}={device}, {nameof(Packet)}={packet})";
    }
}
