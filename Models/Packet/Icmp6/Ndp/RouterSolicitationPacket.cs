﻿using Models.Field;
using Models.Packet.Icmp6.Ndp.Option;
using Models.Util;

namespace Models.Packet.Icmp6.Ndp;

internal class RouterSolicitationPacket : NdpPacket {

    public override List<NdpOption> Options {
        get => ParseOptions(Header.GetNextSegment());
        set => WriteOptions(value, NdpField.RSOptionsPosition);
    }

    public RouterSolicitationPacket(ByteSegment data) : base(data) {
        Header.SegmentLength = NdpField.RSHeaderLength;
        Payload = null;
    }

    public override string ToString() {
        return $@"
{{
    {nameof(Options)} = {PrintOptions()}
}}
        ".Trim();
    }
}
