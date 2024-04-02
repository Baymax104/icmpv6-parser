using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Messaging.Messages;
using Icmpv6.VO;
using Microsoft.Win32;
using SharpPcap;
using SharpPcap.LibPcap;

namespace Icmpv6.ViewModel;

public partial class CaptureListViewModel : ObservableRecipient, IRecipient<ValueChangedMessage<RawCapture>> {

    [ObservableProperty]
    private ObservableCollection<CaptureView> captures = [];

    [ObservableProperty]
    private int selectedIndex = -1;

    public CaptureListViewModel() {
        IsActive = true;
    }

    public void Receive(ValueChangedMessage<RawCapture> message) {
        var view = new CaptureView(message.Value) { Index = Captures.Count + 1 };
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
    private void SaveFile() {
        var dialog = new SaveFileDialog {
            FileName = "capture.pcapng",
            Filter = "Capture Files (*.pcapng, *.pcap)|*.pcapng;*.pcap",
        };
        var result = dialog.ShowDialog();
        if (result != null && result.Value) {
            using var writer = new CaptureFileWriterDevice(dialog.FileName);
            writer.Open();
            foreach (var view in Captures) {
                writer.Write(view.Instance);
            }
        }
    }

    [RelayCommand]
    private void OpenFile() {

    }
}
