using SharpPcap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.model;

internal abstract class NetPacket(ByteSegment packet) {

    protected ByteSegment Header { get; } = packet;

    protected Lazy<NetPacket> PayloadPacket { get; set; } = new();

    public abstract NetPacket ParseNextPacket();

    /// <summary>
    /// 解析包入口方法，从链路层开始解析
    /// </summary>
    /// <param name="rawCapture"><see cref="RawCapture"/>></param>
    /// <returns>实际返回<see cref="EtherPacket"/></returns>
    /// <exception cref="ArgumentException">LinkLayerType is not Ethernet.</exception>
    public static NetPacket ParsePacket(RawCapture rawCapture) {
        ByteSegment packet = new(rawCapture.Data);
        if (rawCapture.LinkLayerType != PacketDotNet.LinkLayers.Ethernet) {
            throw new ArgumentException("LinkLayerType is not Ethernet.");
        }
        return new EtherPacket(packet);
    }

}

