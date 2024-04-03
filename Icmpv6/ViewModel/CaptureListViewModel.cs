using System.Collections.ObjectModel;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using HandyControl.Data;
using Icmpv6.Repo;
using Icmpv6.VO;
using Icmpv6.VO.Messages;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Win32;
using MessageBox = HandyControl.Controls.MessageBox;

namespace Icmpv6.ViewModel;

public partial class CaptureListViewModel : 
    ObservableRecipient, 
    IRecipient<PacketCaptureMessage> {

    private readonly Repository repo = App.Current.Services.GetService<Repository>() ??
                                       throw new NullReferenceException("Repository is null.");

    [ObservableProperty]
    private ObservableCollection<CaptureView> captures = [];

    [ObservableProperty]
    private int selectedIndex = -1;

    public CaptureListViewModel() {
        IsActive = true;
    }

    public void Receive(PacketCaptureMessage message) {
        var view = new CaptureView(message.Value) { Id = Captures.Count + 1 };
        Captures.Add(view);
    }

    [RelayCommand]
    private void SelectPrevious() {
        if (SelectedIndex > 0) {
            SelectedIndex--;
        }
    }

    [RelayCommand]
    private void SelectNext() {
        if (SelectedIndex < Captures.Count - 1) {
            SelectedIndex++;
        }
    }

    [RelayCommand]
    private void SelectFirst() {
        if (Captures.Count > 0) {
            SelectedIndex = 0;
        } else {
            SelectedIndex = -1;
        }
    }

    [RelayCommand]
    private void SelectLast() {
        SelectedIndex = Captures.Count - 1;
    }

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
        }
    }

    [RelayCommand]
    private async Task OpenFile() {
        if (Captures.Count > 0) {
            if (!await SaveBeforeOpenFile()) {
                return;
            }
        }
        var dialog = new OpenFileDialog() { Filter = "Capture Files (*.pcapng, *.pcap)|*.pcapng;*.pcap" };
        var result = dialog.ShowDialog();
        if (result != null && result.Value) {
            var rawCaptures = await Task.Run(() => repo.OpenFile(dialog.FileName));
            Captures.Clear();
            Messenger.Send(new ResetMessage());
            foreach (var rawCapture in rawCaptures) {
                var view = new CaptureView(rawCapture) { Id = Captures.Count + 1 };
                Captures.Add(view);
            }
        }
    }

    private Task<bool> SaveBeforeOpenFile() {
        return Task.Run(
            () => {
                var info = new MessageBoxInfo() {
                    Message = "打开文件会覆盖捕获列表，是否保存当前捕获列表？",
                    Caption = "ICMPv6协议分析器",
                    Button = MessageBoxButton.OKCancel,
                    IconKey = ResourceToken.WarningGeometry,
                    IconBrushKey = ResourceToken.WarningBrush
                };
                var result = MessageBox.Show(info);
                if (result != MessageBoxResult.OK) {
                    return true;
                }
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

    [RelayCommand]
    private void ItemShow(CaptureView item) {
        Messenger.Send(new ShowCaptureMessage(item));
    }
}
