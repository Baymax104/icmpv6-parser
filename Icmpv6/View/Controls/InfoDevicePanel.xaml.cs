using System.Windows;
using System.Windows.Controls;
using Icmpv6.VO;

namespace Icmpv6.View.Controls;

public partial class InfoDevicePanel : UserControl {

    public static readonly DependencyProperty ValueProperty =
        DependencyProperty.Register(
            nameof(Value),
            typeof(DeviceView),
            typeof(InfoDevicePanel),
            new(default(DeviceView)));

    public InfoDevicePanel() {
        InitializeComponent();
        Root.DataContext = this;

        // 处理DataGrid滚动
        ScrollViewer.PreviewMouseWheel += (sender, args) => {
            args.Handled = true;
            var scrollViewer = (ScrollViewer)sender;
            scrollViewer.ScrollToVerticalOffset(scrollViewer.VerticalOffset - args.Delta);
        };
        InfoGrid.PreviewMouseWheel += (_, args) => args.Handled = false;
        AddressGrid.PreviewMouseWheel += (_, args) => args.Handled = false;
    }

    public DeviceView Value {
        get => (DeviceView)GetValue(ValueProperty);
        set => SetValue(ValueProperty, value);
    }
}
