using System.Windows;
using System.Windows.Controls;
using Icmpv6.VO;

namespace Icmpv6.View;

public partial class InfoDevicePanel : UserControl {

    public static readonly DependencyProperty ViewProperty = DependencyProperty.Register(nameof(View), typeof(DeviceView), typeof(InfoDevicePanel), new(default(DeviceView)));
    
    public DeviceView View {
        get => (DeviceView)GetValue(ViewProperty);
        set => SetValue(ViewProperty, value);
    }
    
    public InfoDevicePanel() {
        InitializeComponent();
        DataContext = this;
    }
}

