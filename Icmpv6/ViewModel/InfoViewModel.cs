using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Messaging.Messages;
using Icmpv6.VO;
using Models.Packet;

namespace Icmpv6.ViewModel;

public partial class InfoViewModel : 
    ObservableRecipient, 
    IRecipient<ValueChangedMessage<DeviceView>>,
    IRecipient<ValueChangedMessage<CaptureView>> {

    [ObservableProperty]
    private ObservableCollection<InfoView> infos = [];

    [ObservableProperty]
    private int selectedIndex = -1;

    [ObservableProperty]
    private InfoView? selectedItem;

    public InfoViewModel() {
        IsActive = true;
    }

    public void Receive(ValueChangedMessage<DeviceView> message) {
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

    public void Receive(ValueChangedMessage<CaptureView> message) {
        foreach (var info in Infos) {
            if (info.Type == InfoView.InfoType.Packet && info.Packet.Id == message.Value.Id) {
                SelectedItem = info;
                return;
            }
        }
        if (!message.Value.HasInstance) {
            return;
        }
        var capture = message.Value;
        var packet = NetPacket.ParsePacket(capture.Instance);
        var infoView = new InfoView(new PacketView(packet) { Id = capture.Id });
        Infos.Add(infoView);
        SelectedIndex = Infos.Count - 1;
    }
}
