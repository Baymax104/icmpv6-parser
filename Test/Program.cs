using SharpPcap;
using SharpPcap.LibPcap;
using System;
using Test.model;
using Test.constant;


namespace Test;
internal class Program {

    static CaptureFileWriterDevice writer = new("capture.pcap");

    static void Main(string[] args) {

        CaptureFileReaderDevice reader = new("capture.pcap");

        reader.OnPacketArrival += new(OnPacketArrival);

        reader.Open();

        reader.StartCapture();
        Console.ReadLine();

        reader.StopCapture();

        reader.Close();

    }

    public static void OnPacketArrival(object s, PacketCapture capture) {
        NetPacket packet = NetPacket.ParsePacket(capture.GetPacket());
        EtherPacket? ethernet = packet as EtherPacket;
        if (ethernet != null) {
            Console.WriteLine();
            Console.WriteLine($"src = {ethernet.SourceMacAddress}");
            Console.WriteLine($"dest = {ethernet.DestinationMacAddress}");
            Console.WriteLine($"type = {ethernet.Type}");
            Console.WriteLine("修改");
            ethernet.Type = EtherType.IPv4;
            byte[] bytes = new byte[] { 0x66, 0x77, 0x88, 0x99, 0xaa, 0xbb };
            ethernet.SourceMacAddress = new(bytes);
            ethernet.DestinationMacAddress = new(bytes);
            Console.WriteLine($"src = {ethernet.SourceMacAddress}");
            Console.WriteLine($"dest = {ethernet.DestinationMacAddress}");
            Console.WriteLine($"type = {ethernet.Type}");
        }
    }

}

