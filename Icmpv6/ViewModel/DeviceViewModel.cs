using System.Collections.ObjectModel;
using System.Windows.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using Icmpv6.Repo;
using Icmpv6.View;
using Icmpv6.VO;
using Microsoft.Extensions.DependencyInjection;

namespace Icmpv6.ViewModel;

public partial class DeviceViewModel : ObservableObject {

    private readonly Repository repo = App.Current.Services.GetService<Repository>() ??
                                       throw new NullReferenceException("Repository is null");

    [ObservableProperty]
    private ObservableCollection<DeviceView> devices = [];

    public DeviceViewModel() {
        var devs = repo.GetAllDevices();
        devs.ForEach(d => devices.Add(d.ToView()));
    }
}
