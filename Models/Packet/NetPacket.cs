﻿using Models.Util;
using PacketDotNet;
using SharpPcap;

namespace Models.Packet;

public abstract class NetPacket(ByteSegment packet) {

    protected ByteSegment Header { get; } = packet;

    protected Payload? Payload { get; init; }

    public NetPacket? PayloadPacket {
        get => Payload?.Packet;
    }

    public ByteSegment? PayloadBytes {
        get => Payload?.Bytes;
    }

    protected virtual Payload? ParsePayload() {
        return null;
    }

    /// <summary>
    ///     解析包入口方法，从链路层开始解析
    /// </summary>
    /// <param name="rawCapture"><see cref="RawCapture" />></param>
    /// <returns>实际返回<see cref="EtherPacket" /></returns>
    /// <exception cref="ArgumentException">LinkLayerType is not Ethernet.</exception>
    public static NetPacket ParsePacket(RawCapture rawCapture) {
        ByteSegment segment = new(rawCapture.Data);
        NetPacket packet = rawCapture.LinkLayerType switch {
            LinkLayers.Ethernet => new EtherPacket(segment),
            _ => throw new NotSupportedException("Not Supported LinkLayer Type.")
        };
        return packet;
    }

    public static IEnumerable<NetPacket> FlatExtract(NetPacket? packet) {
        var packets = new List<NetPacket>();
        while (packet != null) {
            packets.Add(packet);
            packet = packet.PayloadPacket;
        }
        return packets;
    }

    public T? Extract<T>() {
        var packet = this;
        while (packet is not null) {
            if (packet is T p) {
                return p;
            }
            packet = packet.PayloadPacket;
        }
        return default;
    }
}
