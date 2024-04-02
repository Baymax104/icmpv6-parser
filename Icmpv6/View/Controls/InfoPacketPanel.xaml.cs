using System.Windows;
using System.Windows.Controls;
using Icmpv6.VO;

namespace Icmpv6.View.Controls;

public partial class InfoPacketPanel : UserControl {

    public static readonly DependencyProperty ValueProperty =
        DependencyProperty.Register(
            nameof(Value),
            typeof(CaptureView),
            typeof(InfoPacketPanel));

    public InfoPacketPanel() {
        InitializeComponent();
        Root.DataContext = this;
    }

    public CaptureView Value {
        get => (CaptureView)GetValue(ValueProperty);
        set => SetValue(ValueProperty, value);
    }
}
