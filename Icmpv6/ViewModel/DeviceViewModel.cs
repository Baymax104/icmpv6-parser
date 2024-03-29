using System.Collections.ObjectModel;
using System.Windows.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using Icmpv6.View;
using Icmpv6.VO;

namespace Icmpv6.ViewModel;

public partial class DeviceViewModel : ObservableObject {

    [ObservableProperty]
    private ObservableCollection<DeviceView> devices = [
        new("Hello1", "ipv4", "ipv6", "mac"),
        new("Hello2", "ipv4", "ipv6", "mac"),
        new("Hello3", "ipv4", "ipv6", "mac"),
        new("Hello4", "ipv4", "ipv6", "mac"),
        new("Hello5", "ipv4", "ipv6", "mac"),
        new("Hello6", "ipv4", "ipv6", "mac"),
        new("Hello7", "ipv4", "ipv6", "mac"),
    ];


    public DeviceViewModel() {

    }
}
