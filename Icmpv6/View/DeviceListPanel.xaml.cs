using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using Icmpv6.ViewModel;
using Microsoft.Extensions.DependencyInjection;

namespace Icmpv6.View;

public partial class DeviceListPanel : UserControl {

    public static readonly DependencyProperty ViewModelProperty =
        DependencyProperty.Register(
            nameof(ViewModel),
            typeof(DeviceListViewModel),
            typeof(DeviceListPanel));

    public DeviceListPanel() {
        InitializeComponent();
        var viewModel = App.Current.Services.GetService<DeviceListViewModel>();
        Root.DataContext = viewModel;
        var binding = new Binding {
            Source = viewModel,
            Path = new(".")
        };
        BindingOperations.SetBinding(this, ViewModelProperty, binding);
    }

    public DeviceListViewModel ViewModel {
        get => (DeviceListViewModel)GetValue(ViewModelProperty);
    }
}
