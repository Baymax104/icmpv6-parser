using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using HandyControl.Controls;
using Icmpv6.VO;
using Icmpv6.VO.Messages;
using Models.Packet;

namespace Icmpv6.ViewModel;

public partial class InfoViewModel : 
    ObservableRecipient, 
    IRecipient<ShowDeviceMessage>,
    IRecipient<ShowCaptureMessage>,
    IRecipient<ResetMessage> {

    [ObservableProperty]
    private ObservableCollection<InfoView> infos = [];

    [ObservableProperty]
    private int selectedIndex = -1;

    [ObservableProperty]
    private InfoView? selectedItem;

    public InfoViewModel() {
        IsActive = true;
    }

    public void Receive(ShowDeviceMessage message) {
        foreach (var info in Infos) {
            if (info.Type == InfoView.InfoType.Device && info.Device == message.Value) {
                SelectedItem = info;
                return;
            }
        }
        var infoView = new InfoView(message.Value);
        Infos.Add(infoView);
        SelectedIndex = Infos.Count - 1;
    }

    public void Receive(ShowCaptureMessage message) {
        if (!message.Value.HasInstance) {
            return;
        }
        foreach (var info in Infos) {
            if (info.Type == InfoView.InfoType.Packet && info.Packet.Id == message.Value.Id) {
                SelectedItem = info;
                return;
            }
        }
        var capture = message.Value;
        var packet = NetPacket.ParsePacket(capture.Instance);
        var infoView = new InfoView(new PacketView(packet) { Id = capture.Id });
        Infos.Add(infoView);
        SelectedIndex = Infos.Count - 1;
    }

    public void Receive(ResetMessage message) {
        for (var i = Infos.Count - 1; i >= 0; i--) {
            if (Infos[i].Type == InfoView.InfoType.Packet) {
                Infos.RemoveAt(i);
            }
        }
        Growl.Success("清空成功");
    }
}
