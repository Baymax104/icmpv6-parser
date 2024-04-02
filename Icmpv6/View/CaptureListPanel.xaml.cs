using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using Icmpv6.ViewModel;
using Microsoft.Extensions.DependencyInjection;

namespace Icmpv6.View;

public partial class CaptureListPanel : UserControl {

    public static readonly DependencyProperty ViewModelProperty =
        DependencyProperty.Register(
            nameof(ViewModel),
            typeof(CaptureListViewModel),
            typeof(CaptureListPanel));

    public CaptureListPanel() {
        InitializeComponent();
        var viewModel = App.Current.Services.GetService<CaptureListViewModel>();
        Root.DataContext = viewModel;
        var binding = new Binding {
            Source = viewModel,
            Path = new(".")
        };
        BindingOperations.SetBinding(this, ViewModelProperty, binding);
    }

    public CaptureListViewModel ViewModel {
        get => (CaptureListViewModel)GetValue(ViewModelProperty);
    }
}
