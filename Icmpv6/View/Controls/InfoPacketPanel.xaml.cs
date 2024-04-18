using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Icmpv6.VO;

namespace Icmpv6.View.Controls;

public partial class InfoPacketPanel : UserControl {

    public static readonly DependencyProperty ValueProperty =
        DependencyProperty.Register(
            nameof(Value),
            typeof(PacketView),
            typeof(InfoPacketPanel));

    public InfoPacketPanel() {
        InitializeComponent();
        Root.DataContext = this;

        // 处理DataGrid滚动
        ScrollViewer.PreviewMouseWheel += (sender, args) => {
            args.Handled = true;
            var scrollViewer = (ScrollViewer)sender;
            scrollViewer.ScrollToVerticalOffset(scrollViewer.VerticalOffset - args.Delta);
        };
    }

    public PacketView Value {
        get => (PacketView)GetValue(ValueProperty);
        set => SetValue(ValueProperty, value);
    }

    private void DataGridPreviewMouseWheel(object sender, MouseWheelEventArgs e) {
        e.Handled = false;
    }
}
