using System.Net;
using Models.Field;
using Models.Packet.Icmp6;
using Models.Type;
using Models.Util;

namespace Models.Packet;

public class Ip6Packet : NetPacket {

    public Ip6Packet(ByteSegment data) : base(data) {
        Header.SegmentLength = Ipv6Field.HeaderLength;
        if (PayloadLength > 0) {
            Payload = ParsePayload();
        }
    }

    public IPAddress SourceAddress {
        get => Header.ToIp6Address(Ipv6Field.SourceAddressPosition);
        set => ByteWriter.WriteTo(Header, value, Ipv6Field.SourceAddressPosition);
    }

    public IPAddress DestinationAddress {
        get => Header.ToIp6Address(Ipv6Field.DestinationAddressPosition);
        set => ByteWriter.WriteTo(Header, value, Ipv6Field.DestinationAddressPosition);
    }

    private uint VersionTrafficClassFlowLabel {
        get => Header.ToUInt32(Ipv6Field.VersionTrafficClassFlowLabelPosition);
        set => ByteWriter.WriteTo(Header, value, Ipv6Field.VersionTrafficClassFlowLabelPosition);
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
        get => Header.ToUInt16(Ipv6Field.PayloadLengthPosition);
        set => ByteWriter.WriteTo(Header, value, Ipv6Field.PayloadLengthPosition);
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
        if (nextSegment.SegmentLength <= 0) {
            return new();
        }
        NetPacket? packet = NextHeader switch {
            ProtocolType.IcmpV6 => new Icmp6Packet(nextSegment),
            ProtocolType.Udp => new UdpPacket(nextSegment),
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
}}
        ".Trim();
    }
}
