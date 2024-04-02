using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Icmpv6.View.Controls;

public partial class Toolbar : UserControl {

    public static readonly DependencyProperty StartCaptureCommandProperty =
        DependencyProperty.Register(
            nameof(StartCaptureCommand),
            typeof(ICommand),
            typeof(Toolbar));

    public static readonly DependencyProperty StopCaptureCommandProperty =
        DependencyProperty.Register(
            nameof(StopCaptureCommand),
            typeof(ICommand),
            typeof(Toolbar));

    public static readonly DependencyProperty PreviousCommandProperty =
        DependencyProperty.Register(
            nameof(PreviousCommand),
            typeof(ICommand),
            typeof(Toolbar));

    public static readonly DependencyProperty NextCommandProperty =
        DependencyProperty.Register(
            nameof(NextCommand),
            typeof(ICommand),
            typeof(Toolbar));

    public static readonly DependencyProperty TopCommandProperty =
        DependencyProperty.Register(
            nameof(TopCommand),
            typeof(ICommand),
            typeof(Toolbar));

    public static readonly DependencyProperty BottomCommandProperty =
        DependencyProperty.Register(
            nameof(BottomCommand),
            typeof(ICommand),
            typeof(Toolbar));

    public static readonly DependencyProperty IsRunningProperty =
        DependencyProperty.Register(
            nameof(IsRunning),
            typeof(bool),
            typeof(Toolbar));

    public Toolbar() {
        InitializeComponent();
        Root.DataContext = this;
    }

    public ICommand StartCaptureCommand {
        get => (ICommand)GetValue(StartCaptureCommandProperty);
        set => SetValue(StartCaptureCommandProperty, value);
    }

    public ICommand StopCaptureCommand {
        get => (ICommand)GetValue(StopCaptureCommandProperty);
        set => SetValue(StopCaptureCommandProperty, value);
    }

    public ICommand PreviousCommand {
        get => (ICommand)GetValue(PreviousCommandProperty);
        set => SetValue(PreviousCommandProperty, value);
    }

    public ICommand NextCommand {
        get => (ICommand)GetValue(NextCommandProperty);
        set => SetValue(NextCommandProperty, value);
    }

    public ICommand TopCommand {
        get => (ICommand)GetValue(TopCommandProperty);
        set => SetValue(TopCommandProperty, value);
    }

    public ICommand BottomCommand {
        get => (ICommand)GetValue(BottomCommandProperty);
        set => SetValue(BottomCommandProperty, value);
    }

    public bool IsRunning {
        get => (bool)GetValue(IsRunningProperty);
        set => SetValue(IsRunningProperty, value);
    }
}
