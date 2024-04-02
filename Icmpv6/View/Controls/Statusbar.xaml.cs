using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using Icmpv6.VO;

namespace Icmpv6.View.Controls;

public partial class Statusbar : UserControl {

    public static readonly DependencyProperty SelectedDeviceProperty =
        DependencyProperty.Register(
            nameof(SelectedDevice),
            typeof(DeviceView),
            typeof(Statusbar),
            new(default(DeviceView)));

    public static readonly DependencyProperty CurrentStatisticsProperty =
        DependencyProperty.Register(
            nameof(CurrentStatistics),
            typeof(StatisticsView),
            typeof(Statusbar));

    public Statusbar() {
        InitializeComponent();
        Root.DataContext = this;
    }

    public DeviceView SelectedDevice {
        get => (DeviceView)GetValue(SelectedDeviceProperty);
        set => SetValue(SelectedDeviceProperty, value);
    }

    public StatisticsView CurrentStatistics {
        get => (StatisticsView)GetValue(CurrentStatisticsProperty);
        set => SetValue(CurrentStatisticsProperty, value);
    }
}
