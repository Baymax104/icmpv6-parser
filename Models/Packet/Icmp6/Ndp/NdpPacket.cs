using Models.Field;
using Models.Packet.Icmp6.Ndp.Option;
using Models.Type;
using Models.Util;

namespace Models.Packet.Icmp6.Ndp;

public abstract class NdpPacket(ByteSegment data) : NetPacket(data) {

    public virtual List<NdpOption> Options { get; set; } = [];

    protected List<NdpOption> ParseOptions(ByteSegment optionSegment) {
        List<NdpOption> options = [];
        if (optionSegment.SegmentLength == 0) {
            return options;
        }

        var offset = 0;
        var ending = optionSegment.SegmentLength;

        while (offset < ending) {
            var type = (NdpOptionType)optionSegment[offset + NdpOptionField.TypePosition];
            var length = optionSegment[offset + NdpOptionField.LengthPosition] * 8;
            var actualOffset = optionSegment.Offset + offset;
            var actualLength = actualOffset + length;
            var segment = new ByteSegment(optionSegment.Data, actualOffset, length, actualLength);
            NdpOption option = type switch {
                NdpOptionType.SourceLinkLayerAddress => new LinkLayerAddressOption(segment),
                NdpOptionType.TargetLinkLayerAddress => new LinkLayerAddressOption(segment),
                NdpOptionType.PrefixInformation => new PrefixInformationOption(segment),
                NdpOptionType.RedirectedHeader => new RedirectedHeaderOption(segment),
                NdpOptionType.Mtu => new MtuOption(segment),
                _ => throw new NotSupportedException(nameof(type))
            };
            options.Add(option);
            offset += length;
        }

        return options;
    }

    protected void WriteOptions(List<NdpOption> options, int offset) {
        var start = Header.Offset + offset;
        foreach (var item in options) {
            var bytes = item.ActualBytes;
            Array.Copy(bytes, 0, Header.Data, start, bytes.Length);
            start += bytes.Length;
        }
    }

}
