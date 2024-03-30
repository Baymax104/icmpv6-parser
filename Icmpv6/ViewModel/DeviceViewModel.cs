using System.Collections.ObjectModel;
using System.Windows.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using Icmpv6.View;
using Icmpv6.VO;

namespace Icmpv6.ViewModel;

public partial class DeviceViewModel : ObservableObject {

    [ObservableProperty]
    private ObservableCollection<DeviceView> devices = [];


    public DeviceViewModel() {

    }
}
