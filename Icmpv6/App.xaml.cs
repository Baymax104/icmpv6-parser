using System.Windows;
using Icmpv6.Repo;
using Icmpv6.ViewModel;
using Microsoft.Extensions.DependencyInjection;

namespace Icmpv6;

/// <summary>
///     Interaction logic for App.xaml
/// </summary>
public partial class App : Application {

    public new static App Current {
        get => (App)Application.Current;
    }

    public IServiceProvider Services { get; } = ConfigureServices();

    private static IServiceProvider ConfigureServices() {
        var services = new ServiceCollection()
            .AddSingleton<Repository>()
            .AddTransient<CaptureListViewModel>()
            .AddTransient<DeviceListViewModel>()
            .AddTransient<InfoViewModel>();

        return services.BuildServiceProvider();
    }
}
