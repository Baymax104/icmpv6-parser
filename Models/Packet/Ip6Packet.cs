using System.Net;
using Models.Field;
using Models.Type;
using Models.Unit;

namespace Models.Packet;

public class Ip6Packet : NetPacket {

    public readonly IpVersion IpVersion = IpVersion.IPv6;


    public Ip6Packet(ByteSegment data) : base(data) {
        Header.SegmentLength = Ipv6Field.HeaderLength;
        if (PayloadLength > 0) {
            Payload = ParsePayload();
        }
    }

    public IPAddress SourceAddress {
        get {
            var span = Header.AsSpan(Ipv6Field.SourceAddressPosition, Ipv6Field.AddressLength);
            return new(span);
        }
        set {
            var bytes = value.GetAddressBytes();
            var start = Header.Offset + Ipv6Field.SourceAddressPosition;
            Array.Copy(bytes, 0, Header.Data, start, Ipv6Field.AddressLength);
        }
    }

    public IPAddress DestinationAddress {
        get {
            var span = Header.AsSpan(Ipv6Field.DestinationAddressPosition, Ipv6Field.AddressLength);
            return new(span);
        }
        set {
            var bytes = value.GetAddressBytes();
            var start = Header.Offset + Ipv6Field.DestinationAddressPosition;
            Array.Copy(bytes, 0, Header.Data, start, Ipv6Field.AddressLength);
        }
    }

    private uint VersionTrafficClassFlowLabel {
        get {
            var bytes = new byte[Ipv6Field.VersionTrafficClassFlowLabelLength];
            var start = Header.Offset + Ipv6Field.VersionTrafficClassFlowLabelPosition;
            Array.Copy(Header.Data, start, bytes, 0, Ipv6Field.VersionTrafficClassFlowLabelLength);
            // 注意包内大端序，转换小端序
            Array.Reverse(bytes);
            return BitConverter.ToUInt32(bytes, 0);
        }
        set {
            var bytes = BitConverter.GetBytes(value);
            Array.Reverse(bytes);
            var start = Header.Offset + Ipv6Field.VersionTrafficClassFlowLabelPosition;
            Array.Copy(bytes, 0, Header.Data, start, Ipv6Field.VersionTrafficClassFlowLabelLength);
        }
    }

    public IpVersion Version {
        get => (IpVersion)((VersionTrafficClassFlowLabel >> 28) & 0xF);
        set => VersionTrafficClassFlowLabel = (VersionTrafficClassFlowLabel & 0x0FFFFFFFu) | (((uint)value << 28) & 0xF0000000u);
    }

    public uint TrafficClass {
        get => (VersionTrafficClassFlowLabel >> 20) & 0xFF;
        set => VersionTrafficClassFlowLabel = (VersionTrafficClassFlowLabel & 0xF00FFFFFu) | ((value << 20) & 0xFF00000u);
    }

    public uint FlowLabel {
        get => VersionTrafficClassFlowLabel & 0x000FFFFF;
        set => VersionTrafficClassFlowLabel = (VersionTrafficClassFlowLabel & 0xFFF00000u) | (value & 0x000FFFFFu);
    }

    public ushort PayloadLength {
        get {
            var bytes = new byte[Ipv6Field.PayloadLengthLength];
            var start = Header.Offset + Ipv6Field.PayloadLengthPosition;
            Array.Copy(Header.Data, start, bytes, 0, Ipv6Field.PayloadLengthLength);
            Array.Reverse(bytes);
            return BitConverter.ToUInt16(bytes, 0);
        }
    }

    public ProtocolType NextHeader {
        get => (ProtocolType)Header[Ipv6Field.NextHeaderPosition];
        set => Header[Ipv6Field.NextHeaderPosition] = (byte)value;
    }

    public int HopLimit {
        get => Header[Ipv6Field.HopLimitPosition];
        set => Header[Ipv6Field.HopLimitPosition] = (byte)value;
    }

    protected override sealed Payload ParsePayload() {
        var nextSegment = Header.GetNextSegment();
        NetPacket? packet = NextHeader switch {
            ProtocolType.IcmpV6 => new Icmp6Packet(nextSegment),
            _ => null
        };
        return packet is not null ? new(packet) : new(nextSegment);
    }

    public override string ToString() {
        return $@"
{{
    {nameof(Version)} = {Version},
    {nameof(TrafficClass)} = {TrafficClass},
    {nameof(FlowLabel)} = {FlowLabel}
    {nameof(PayloadLength)} = {PayloadLength},
    {nameof(NextHeader)} = {NextHeader},
    {nameof(HopLimit)} = {HopLimit},
    {nameof(SourceAddress)} = {SourceAddress},
    {nameof(DestinationAddress)} = {DestinationAddress},
}}".Trim();
    }
}
