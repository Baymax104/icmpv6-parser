using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using HandyControl.Controls;
using Icmpv6.Repo;
using Icmpv6.VO;
using Icmpv6.VO.Messages;
using Microsoft.Extensions.DependencyInjection;
using SharpPcap;

namespace Icmpv6.ViewModel;

public partial class DeviceListViewModel : ObservableRecipient {

    private readonly Repository repo = App.Current.Services.GetService<Repository>() ??
                                       throw new NullReferenceException("Repository is null");

    [ObservableProperty]
    private ObservableCollection<DeviceView> devices = [];

    [ObservableProperty]
    private DeviceView? selectedItem;

    [ObservableProperty]
    [NotifyPropertyChangedRecipients]
    private DeviceView? currentCaptureDevice;

    [ObservableProperty]
    private StatisticsView statistics = new();

    public DeviceListViewModel() {
        IsActive = true;
        var devs = repo.GetAllDevices();
        devs.ForEach(d => devices.Add(new(d)));
    }

    [RelayCommand]
    private void ItemShow(DeviceView item) {
        Messenger.Send(new ShowDeviceMessage(item));
    }

    [RelayCommand(IncludeCancelCommand = true)]
    private async Task CaptureAsync(CancellationToken token) {
        if (SelectedItem == null || !SelectedItem.HasInstance) {
            Growl.Warning("当前未选择设备");
            return;
        }
        CurrentCaptureDevice = SelectedItem;
        var device = CurrentCaptureDevice.Instance;
        device.Open(DeviceModes.Promiscuous);
        device.Filter = "icmp6";
        try {
            while (true) {
                Statistics = GetStatisticsView(device);
                var packet = await repo.CaptureAsync(device, token);
                if (packet != null) {
                    Messenger.Send(new PacketCaptureMessage(packet));
                }
            }
        } catch (OperationCanceledException) {
            // Ignored
        } finally {
            device.Close();
            CurrentCaptureDevice = null;
        }
    }

    private StatisticsView GetStatisticsView(ICaptureDevice device) {
        var captured = device.Statistics.ReceivedPackets;
        var dropped = device.Statistics.DroppedPackets;
        var capturedProportion = (uint)(captured * 1.0 / (captured + dropped) * 100);
        var droppedProportion = (uint)(dropped * 1.0 / (captured + dropped) * 100);
        return new() {
            CapturedPackets = captured,
            DroppedPackets = dropped,
            CapturedProportion = capturedProportion,
            DroppedProportion = droppedProportion
        };
    }
}
