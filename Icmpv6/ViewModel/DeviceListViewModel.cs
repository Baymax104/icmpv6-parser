using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Icmpv6.Repo;
using Icmpv6.VO;
using Icmpv6.VO.Messages;
using Microsoft.Extensions.DependencyInjection;

namespace Icmpv6.ViewModel;

public partial class DeviceListViewModel : ObservableRecipient {

    private readonly Repository repo = App.Current.Services.GetService<Repository>() ??
                                       throw new NullReferenceException("Repository is null");

    [ObservableProperty]
    private ObservableCollection<DeviceView> devices = [];

    [ObservableProperty]
    [NotifyPropertyChangedRecipients]
    private DeviceView? selectedItem;

    public DeviceListViewModel() {
        IsActive = true;
        var devs = repo.GetAllDevices();
        devs.ForEach(d => devices.Add(new(d)));
    }

    [RelayCommand]
    private void ItemShow(DeviceView item) {
        Messenger.Send(new ShowDeviceMessage(item));
    }
}
