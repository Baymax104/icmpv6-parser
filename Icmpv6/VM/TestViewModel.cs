using CommunityToolkit.Mvvm.ComponentModel;

namespace Icmpv6.VM;

public partial class TestViewModel : ObservableObject {

    [ObservableProperty]
    private int count;
}
