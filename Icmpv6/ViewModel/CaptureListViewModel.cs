using System.Collections.ObjectModel;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Messaging.Messages;
using HandyControl.Controls;
using HandyControl.Data;
using Icmpv6.Repo;
using Icmpv6.VO;
using Icmpv6.VO.Messages;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Win32;
using SharpPcap;
using MessageBox = HandyControl.Controls.MessageBox;

namespace Icmpv6.ViewModel;

public partial class CaptureListViewModel : 
    ObservableRecipient, 
    IRecipient<PropertyChangedMessage<DeviceView?>> {

    private readonly Repository repo = App.Current.Services.GetService<Repository>() ??
                                       throw new NullReferenceException("Repository is null.");

    [ObservableProperty]
    private ObservableCollection<CaptureView> captures = [];

    [ObservableProperty]
    private int selectedCaptureIndex = -1;

    [ObservableProperty]
    private DeviceView? selectedDevice;

    [ObservableProperty]
    private DeviceView? currentCaptureDevice;
    
    [ObservableProperty]
    private StatisticsView statistics = new();

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(ClearCaptureCommand))]
    private bool hasCaptures;

    [ObservableProperty]
    private bool isCapturing;

    [ObservableProperty]
    private string captureDeviceName = "数据包捕获列表";

    [ObservableProperty]
    private bool isCaptureLoading;

    public CaptureListViewModel() {
        IsActive = true;
        Captures.CollectionChanged += (_, _) => HasCaptures = Captures.Count != 0;
    }

    public void Receive(PropertyChangedMessage<DeviceView?> message) {
        SelectedDevice = message.NewValue;
    }

    partial void OnCurrentCaptureDeviceChanged(DeviceView? value) {
        IsCapturing = value != null;
        IsCaptureLoading = IsCapturing && !HasCaptures;
        CaptureDeviceName = value != null ? $"{value.Name} 正在捕获中" : "数据包捕获列表";
    }

    partial void OnHasCapturesChanged(bool value) {
        IsCaptureLoading = !value && IsCapturing;
    }
    
    [RelayCommand]
    private void ItemShow(CaptureView item) {
        Messenger.Send(new ShowCaptureMessage(item));
    }

    #region 数据包捕获命令

    [RelayCommand(IncludeCancelCommand = true)]
    private async Task CaptureAsync(CancellationToken token) {
        if (SelectedDevice == null || !SelectedDevice.HasInstance) {
            Growl.Warning("当前未选择设备");
            return;
        }
        CurrentCaptureDevice = SelectedDevice;
        var device = CurrentCaptureDevice.Instance;
        device.Open(DeviceModes.Promiscuous);
        device.Filter = "icmp6";
        try {
            while (true) {
                Statistics = GetStatisticsView(device);
                var packet = await repo.CaptureAsync(device, token);
                if (packet != null) {
                    var view = new CaptureView(packet) { Id = Captures.Count + 1 };
                    Captures.Add(view);
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

    [RelayCommand(CanExecute = nameof(HasCaptures))]
    private void ClearCapture() {
        var info = new MessageBoxInfo() {
            Message = "确认清空当前捕获数据包吗？",
            Caption = "ICMPv6协议分析器",
            Button = MessageBoxButton.OKCancel,
            IconKey = ResourceToken.WarningGeometry,
            IconBrushKey = ResourceToken.WarningBrush
        };
        if (MessageBox.Show(info) == MessageBoxResult.OK) {
            Messenger.Send(new ResetMessage());
            Captures.Clear();
            Statistics = new();
        }
    }

    #endregion

    #region 选择数据包命令

    [RelayCommand]
    private void SelectPrevious() {
        if (SelectedCaptureIndex > 0) {
            SelectedCaptureIndex--;
        }
    }

    [RelayCommand]
    private void SelectNext() {
        if (SelectedCaptureIndex < Captures.Count - 1) {
            SelectedCaptureIndex++;
        }
    }

    [RelayCommand]
    private void SelectFirst() {
        if (Captures.Count > 0) {
            SelectedCaptureIndex = 0;
        } else {
            SelectedCaptureIndex = -1;
        }
    }

    [RelayCommand]
    private void SelectLast() {
        SelectedCaptureIndex = Captures.Count - 1;
    }

    #endregion

    #region 文件操作命令

    [RelayCommand]
    private async Task SaveFile() {
        var dialog = new SaveFileDialog {
            FileName = "capture.pcapng",
            Filter = "Capture Files (*.pcapng, *.pcap)|*.pcapng;*.pcap",
        };
        var result = dialog.ShowDialog();
        if (result != null && result.Value) {
            var rawCaptures = Captures.Select(v => v.Instance);
            await Task.Run(() => repo.SaveFile(dialog.FileName, rawCaptures));
            Growl.Success("保存成功");
        }
    }

    [RelayCommand]
    private async Task OpenFile() {
        if (Captures.Count > 0) {
            var info = new MessageBoxInfo() {
                Message = "打开文件会覆盖捕获列表，是否保存当前捕获列表？",
                Caption = "ICMPv6协议分析器",
                Button = MessageBoxButton.OKCancel,
                IconKey = ResourceToken.WarningGeometry,
                IconBrushKey = ResourceToken.WarningBrush
            };
            if (MessageBox.Show(info) == MessageBoxResult.OK) {
                if (await SaveBeforeOpenFile()) {
                    Growl.Success("保存成功，正在打开文件...");
                } else {
                    Growl.Info("保存操作取消，正在打开文件...");
                }
                await Task.Delay(500);
            }
        }
        var dialog = new OpenFileDialog() { Filter = "Capture Files (*.pcapng, *.pcap)|*.pcapng;*.pcap" };
        var openResult = dialog.ShowDialog();
        if (openResult != null && openResult.Value) {
            var rawCaptures = await Task.Run(() => repo.OpenFile(dialog.FileName));
            Captures.Clear();
            Messenger.Send(new ResetMessage());
            foreach (var rawCapture in rawCaptures) {
                var view = new CaptureView(rawCapture) { Id = Captures.Count + 1 };
                Captures.Add(view);
            }
            Growl.Success("打开文件成功");
        }
    }

    private Task<bool> SaveBeforeOpenFile() {
        return Task.Run(
            () => {
                var dialog = new SaveFileDialog {
                    FileName = "capture.pcapng",
                    Filter = "Capture Files (*.pcapng, *.pcap)|*.pcapng;*.pcap",
                };
                var dialogResult = dialog.ShowDialog();
                if (dialogResult == null || !dialogResult.Value) {
                    return false;
                }
                var rawCaptures = Captures.Select(v => v.Instance);
                repo.SaveFile(dialog.FileName, rawCaptures);
                return true;
            });
    }

    #endregion
}
