﻿using System.Windows.Controls;
using Icmpv6.ViewModel;
using Microsoft.Extensions.DependencyInjection;

namespace Icmpv6.View;

public partial class DeviceListPanel : UserControl {

    public DeviceListPanel() {
        InitializeComponent();
        Root.DataContext = App.Current.Services.GetService<DeviceListViewModel>();
    }
}
