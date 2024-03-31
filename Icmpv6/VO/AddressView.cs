namespace Icmpv6.VO;

public record AddressView {
    public string Address { get; set; } = "";

    public string Netmask { get; set; } = "";

    public string Broadcast { get; set; } = "";

    public string Type { get; set; } = "";
}
