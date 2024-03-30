using System.Windows.Controls;
using Icmpv6.ViewModel;
using Microsoft.Extensions.DependencyInjection;

namespace Icmpv6.View;

public partial class InfoPanel : UserControl {
    public InfoPanel() {
        InitializeComponent();
        DataContext = App.Current.Services.GetService<InfoViewModel>();
    }
}
