using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using Icmpv6.VO;

namespace Icmpv6.ViewModel;

public partial class InfoViewModel : ObservableObject {

    [ObservableProperty]
    private ObservableCollection<InfoView> infos = [
        new("Hello1"),
        new("Hello2"),
        new("Hello3"),
        new("Hello4"),
        new("Hello5"),
        new("Hello6"),
        new(new DeviceView("Device", "", "", ""))
    ];

    [ObservableProperty]
    private int selected;

    public InfoViewModel() {
        
    }
}
