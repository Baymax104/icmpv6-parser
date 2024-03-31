using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Messaging.Messages;
using Icmpv6.VO;

namespace Icmpv6.ViewModel;

public partial class InfoViewModel : ObservableRecipient, IRecipient<ValueChangedMessage<DeviceView>> {

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
        foreach (var info in Infos)
            if (info.Device == message.Value) {
                SelectedItem = info;
                return;
            }
        var infoView = new InfoView(message.Value);
        Infos.Add(infoView);
        SelectedIndex = Infos.Count - 1;
    }
}
