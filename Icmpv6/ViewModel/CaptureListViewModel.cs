using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Messaging.Messages;
using Icmpv6.VO;
using SharpPcap;

namespace Icmpv6.ViewModel;

public partial class CaptureListViewModel : ObservableRecipient, IRecipient<ValueChangedMessage<RawCapture>> {

    [ObservableProperty]
    private ObservableCollection<CaptureView> captures = [];

    public CaptureListViewModel() {
        IsActive = true;
    }

    public void Receive(ValueChangedMessage<RawCapture> message) {
        var view = new CaptureView(message.Value) { Index = Captures.Count + 1 };
        Captures.Add(view);
    }
}
