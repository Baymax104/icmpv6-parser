using Models.Constant;
using Models.Unit;
using System.Net;


namespace Models.Packet;

internal class Ip6Packet : NetPacket {
    
    public readonly IpVersion IpVersion = IpVersion.IPv6;

    public IPAddress SourceAddress {
        get {
            var span = Header.Data.AsSpan(Header.Offset + Ipv6Fields.SourceAddressPosition, Ipv6Fields.AddressLength);
            return new(span);
        }
        set {
            byte[] bytes = value.GetAddressBytes();
            int start = Header.Offset + Ipv6Fields.SourceAddressPosition;
            Array.Copy(bytes, 0, Header.Data, start, Ipv6Fields.AddressLength);
        }
    }

    public IPAddress DestinationAddress {
        get {
            var span = Header.Data.AsSpan(Header.Offset + Ipv6Fields.DestinationAddressPosition, Ipv6Fields.AddressLength);
            return new(span);
        }
        set {
            byte[] bytes = value.GetAddressBytes();
            int start = Header.Offset + Ipv6Fields.DestinationAddressPosition;
            Array.Copy(bytes, 0, Header.Data, start, Ipv6Fields.AddressLength);
        }
    }

    private int VersionTrafficClassFlowLabel {
        get {
            byte[] bytes = new byte[Ipv6Fields.VersionTrafficClassFlowLabelLength];
            int start = Header.Offset + Ipv6Fields.VersionTrafficClassFlowLabelPosition;
            Array.Copy(Header.Data, start, bytes, 0, Ipv6Fields.VersionTrafficClassFlowLabelLength);
            // 注意包内大端序，转换小端序
            Array.Reverse(bytes);
            return BitConverter.ToInt32(bytes, 0);
        }
        set {
            byte[] bytes = BitConverter.GetBytes(value);
            Array.Reverse(bytes);
            int start = Header.Offset + Ipv6Fields.VersionTrafficClassFlowLabelPosition;
            Array.Copy(bytes, 0, Header.Data, start, Ipv6Fields.VersionTrafficClassFlowLabelLength);
        }
    }

    public IpVersion Version {
        get => (IpVersion)((VersionTrafficClassFlowLabel >> 28) & 0xF);
        set {
            uint versionTrafficClassFlowLabel = (uint)VersionTrafficClassFlowLabel;
            versionTrafficClassFlowLabel = (uint)((versionTrafficClassFlowLabel & 0x0FFFFFFFu) | (((int)value << 28) & 0xF0000000u));
            VersionTrafficClassFlowLabel = (int)versionTrafficClassFlowLabel;
        }
    }

    public int TrafficClass {
        get => (VersionTrafficClassFlowLabel >> 20) & 0xFF;
        set {
            uint versionTrafficClassFlowLabel = (uint)VersionTrafficClassFlowLabel;
            versionTrafficClassFlowLabel = (versionTrafficClassFlowLabel & 0xF00FFFFFu) | ((uint)(value << 20) & 0xFF00000u);
            VersionTrafficClassFlowLabel = (int)versionTrafficClassFlowLabel;
        }
    }

    public int FlowLabel {
        get => VersionTrafficClassFlowLabel & 0x000FFFFF;
        set {
            uint versionTrafficClassFlowLabel = (uint)VersionTrafficClassFlowLabel;
            versionTrafficClassFlowLabel = (versionTrafficClassFlowLabel & 0xFFF00000u) | ((uint)value & 0x000FFFFFu);
            VersionTrafficClassFlowLabel = (int)versionTrafficClassFlowLabel;
        }
    }

    public ushort PayloadLength {
        get {
            byte[] bytes = new byte[Ipv6Fields.PayloadLengthLength];
            int start = Header.Offset + Ipv6Fields.PayloadLengthPosition;
            Array.Copy(Header.Data, start, bytes, 0, Ipv6Fields.PayloadLengthLength);
            Array.Reverse(bytes);
            return (ushort)BitConverter.ToInt16(bytes, 0);
        }
    }

    public ProtocolType NextHeader {
        get => (ProtocolType)Header[Ipv6Fields.NextHeaderPosition];
        set {
            Header[Ipv6Fields.NextHeaderPosition] = (byte)value;
        }
    }

    public int HopLimit {
        get => Header[Ipv6Fields.HopLimitPosition];
        set {
            Header[Ipv6Fields.HopLimitPosition] = (byte)value;
        }
    }


    public Ip6Packet(ByteSegment data) : base(data) {
        Header.SegmentLength = Ipv6Fields.HeaderLength;
        if (PayloadLength > 0) {
            Payload = ParseNextPayload();
        }
    }

    public override Payload ParseNextPayload() {
        ByteSegment nextSegment = Header.GetNextSegment();
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

