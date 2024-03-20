using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.Unit;
using Models.Field;
using Models.Packet.Ndp.Option;

namespace Models.Packet.Ndp;
internal class RouterAdvertisementPacket : NdpPacket {
    
    public byte CurHopLimit {
        get => Header[NdpField.RACurHopLimitPosition];
        set {
            Header[NdpField.RACurHopLimitPosition] = value;
        }
    }

    public bool ManagedAddressConfiguration {
        get => (Header[NdpField.RAMOPosition] & 0x80) != 0;
        set {
            if (value) {
                Header[NdpField.RAMOPosition] |= 0x80;
            } else {
                Header[NdpField.RAMOPosition] &= 0x7F;
            }
        }
    }

    public bool OtherConfiguration {
        get => (Header[NdpField.RAMOPosition] & 0x40) != 0;
        set {
            if (value) {
                Header[NdpField.RAMOPosition] |= 0x40;
            } else {
                Header[NdpField.RAMOPosition] &= 0xBF;
            }
        }
    }

    public ushort RouterLifetime {
        get {
            byte[] bytes = new byte[NdpField.RARouterLifetimeLength];
            int start = Header.Offset + NdpField.RARouterLifetimePosition;
            Array.Copy(Header.Data, start, bytes, 0, NdpField.RARouterLifetimeLength);
            Array.Reverse(bytes);
            return BitConverter.ToUInt16(bytes, 0);
        }
        set {
            byte[] bytes = BitConverter.GetBytes(value);
            Array.Reverse(bytes);
            int start = Header.Offset + NdpField.RARouterLifetimePosition;
            Array.Copy(bytes, 0, Header.Data, start, NdpField.RARouterLifetimeLength);
        }
    }

    public uint ReachableTime {
        get {
            byte[] bytes = new byte[NdpField.RAReachableTimeLength];
            int start = Header.Offset + NdpField.RAReachableTimePosition;
            Array.Copy(Header.Data, start, bytes, 0, NdpField.RAReachableTimeLength);
            Array.Reverse(bytes);
            return BitConverter.ToUInt32(bytes, 0);
        }
        set {
            byte[] bytes = BitConverter.GetBytes(value);
            Array.Reverse(bytes);
            int start = Header.Offset + NdpField.RARouterLifetimePosition;
            Array.Copy(bytes, 0, Header.Data, start, NdpField.RAReachableTimeLength);
        }
    }

    public uint RetransmitTimer {
        get {
            byte[] bytes = new byte[NdpField.RARetransmitTimerLength];
            int start = Header.Offset + NdpField.RARetransmitTimerPosition;
            Array.Copy(Header.Data, start, bytes, 0, NdpField.RARetransmitTimerLength);
            Array.Reverse(bytes);
            return BitConverter.ToUInt32(bytes, 0);
        }
        set {
            byte[] bytes = BitConverter.GetBytes(value);
            Array.Reverse(bytes);
            int start = Header.Offset + NdpField.RARetransmitTimerPosition;
            Array.Copy(bytes, 0, Header.Data, start, NdpField.RARetransmitTimerLength);
        }
    }

    public override List<NdpOption> Options {
        get => ParseOptions(Header.GetNextSegment());
        set {
            WriteOptions(value, NdpField.RAOptionsPosition);
        }
    }

    public RouterAdvertisementPacket(ByteSegment data) : base(data) { 
        Header.SegmentLength = NdpField.RAHeaderLength;
    }

    public override string ToString() {
        return $@"
{{
    {nameof(CurHopLimit)} = {CurHopLimit},
    {nameof(ManagedAddressConfiguration)} = {ManagedAddressConfiguration},
    {nameof(OtherConfiguration)} = {OtherConfiguration},
    {nameof(RouterLifetime)} = {RouterLifetime},
    {nameof(ReachableTime)} = {ReachableTime},
    {nameof(RetransmitTimer)} = {RetransmitTimer}
    {nameof(Options)} = {Options.Count}
}}
        ".Trim();
    }
}

