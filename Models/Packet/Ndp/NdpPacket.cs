using Models.Field;
using Models.Packet.Ndp.Option;
using Models.Packet.Ndp.Option.Option;
using Models.Type;
using Models.Unit;

namespace Models.Packet.Ndp;
internal abstract class NdpPacket(ByteSegment data) : NetPacket(data) {

    public virtual List<NdpOption> Options { get; set; } = [];

    protected List<NdpOption> ParseOptions(ByteSegment optionSegment) {
        List<NdpOption> options = [];
        if (optionSegment.SegmentLength == 0) {
            return options;
        }

        int offset = 0;
        int ending = optionSegment.SegmentLength;

        while (offset < ending) {
            var type = (NdpOptionType)optionSegment[offset + NdpOptionField.TypePosition];
            var length = optionSegment[offset + NdpOptionField.LengthPosition] * 8;
            int actualOffset = optionSegment.Offset + offset;
            int actualLength = actualOffset + length;
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
        int start = Header.Offset + offset;
        foreach (var item in options) {
            byte[] bytes = item.ActualBytes;
            Array.Copy(bytes, 0, Header.Data, start, bytes.Length);
            start += bytes.Length;
        }
    }

}

