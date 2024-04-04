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

    public static readonly DependencyProperty SelectPreviousCommandProperty =
        DependencyProperty.Register(
            nameof(SelectPreviousCommand),
            typeof(ICommand),
            typeof(Toolbar));

    public static readonly DependencyProperty SelectNextCommandProperty =
        DependencyProperty.Register(
            nameof(SelectNextCommand),
            typeof(ICommand),
            typeof(Toolbar));

    public static readonly DependencyProperty SelectFirstCommandProperty =
        DependencyProperty.Register(
            nameof(SelectFirstCommand),
            typeof(ICommand),
            typeof(Toolbar));

    public static readonly DependencyProperty SelectLastCommandProperty =
        DependencyProperty.Register(
            nameof(SelectLastCommand),
            typeof(ICommand),
            typeof(Toolbar));

    public static readonly DependencyProperty SaveFileCommandProperty =
        DependencyProperty.Register(
            nameof(SaveFileCommand),
            typeof(ICommand),
            typeof(Toolbar));

    public static readonly DependencyProperty OpenFileCommandProperty =
        DependencyProperty.Register(
            nameof(OpenFileCommand),
            typeof(ICommand),
            typeof(Toolbar));
    
    public static readonly DependencyProperty ClearCaptureCommandProperty =
        DependencyProperty.Register(
            nameof(ClearCaptureCommand),
            typeof(ICommand),
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

    public ICommand SelectPreviousCommand {
        get => (ICommand)GetValue(SelectPreviousCommandProperty);
        set => SetValue(SelectPreviousCommandProperty, value);
    }

    public ICommand SelectNextCommand {
        get => (ICommand)GetValue(SelectNextCommandProperty);
        set => SetValue(SelectNextCommandProperty, value);
    }

    public ICommand SelectFirstCommand {
        get => (ICommand)GetValue(SelectFirstCommandProperty);
        set => SetValue(SelectFirstCommandProperty, value);
    }

    public ICommand SelectLastCommand {
        get => (ICommand)GetValue(SelectLastCommandProperty);
        set => SetValue(SelectLastCommandProperty, value);
    }

    public ICommand SaveFileCommand {
        get => (ICommand)GetValue(SaveFileCommandProperty);
        set => SetValue(SaveFileCommandProperty, value);
    }

    public ICommand OpenFileCommand {
        get => (ICommand)GetValue(OpenFileCommandProperty);
        set => SetValue(OpenFileCommandProperty, value);
    }

    public ICommand ClearCaptureCommand {
        get => (ICommand)GetValue(ClearCaptureCommandProperty);
        set => SetValue(ClearCaptureCommandProperty, value);
    }
}
