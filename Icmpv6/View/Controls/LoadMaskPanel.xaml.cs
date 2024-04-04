using System.Windows;
using System.Windows.Controls;

namespace Icmpv6.View.Controls;

public partial class LoadMaskPanel : UserControl {
    
    public static readonly DependencyProperty IsLoadingProperty =
        DependencyProperty.Register(
            nameof(IsLoading),
            typeof(bool),
            typeof(LoadMaskPanel));

    public bool IsLoading {
        get => (bool)GetValue(IsLoadingProperty);
        set => SetValue(IsLoadingProperty, value);
    }
    
    public LoadMaskPanel() {
        InitializeComponent();
        Root.DataContext = this;
    }
}

