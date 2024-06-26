﻿using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using Icmpv6.ViewModel;
using Microsoft.Extensions.DependencyInjection;

namespace Icmpv6.View;

public partial class InfoPanel : UserControl {

    public static readonly DependencyProperty ViewModelProperty =
        DependencyProperty.Register(
            nameof(ViewModel),
            typeof(InfoViewModel),
            typeof(InfoPanel));

    public InfoPanel() {
        InitializeComponent();
        var viewModel = App.Current.Services.GetService<InfoViewModel>();
        Root.DataContext = viewModel;
        var binding = new Binding {
            Source = viewModel,
            Path = new(".")
        };
        BindingOperations.SetBinding(this, ViewModelProperty, binding);
    }

    public InfoViewModel ViewModel {
        get => (InfoViewModel)GetValue(ViewModelProperty);
    }
}
