using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Icmpv6.View.Controls;

public partial class Toolbar : UserControl {

    public static readonly DependencyProperty CaptureCommandProperty =
        DependencyProperty.Register(
            nameof(CaptureCommand),
            typeof(ICommand),
            typeof(Toolbar));

    public static readonly DependencyProperty StopCommandProperty =
        DependencyProperty.Register(
            nameof(StopCommand),
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

    public Toolbar() {
        InitializeComponent();
        Root.DataContext = this;
    }

    public ICommand CaptureCommand {
        get => (ICommand)GetValue(CaptureCommandProperty);
        set => SetValue(CaptureCommandProperty, value);
    }

    public ICommand StopCommand {
        get => (ICommand)GetValue(StopCommandProperty);
        set => SetValue(StopCommandProperty, value);
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
}
