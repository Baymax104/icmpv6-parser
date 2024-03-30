using System.Windows;
using System.Windows.Controls;
using Icmpv6.VO;

namespace Icmpv6.ViewModel;

public class InfoTemplateSelector : DataTemplateSelector {

    public DataTemplate DeviceTemplate { get; set; } = new();

    public DataTemplate PacketTemplate { get; set; } = new();

    public override DataTemplate? SelectTemplate(object? obj, DependencyObject container) {
        if (obj is InfoView item) {
            var template = item.Type switch {
                InfoView.InfoViewType.Device => DeviceTemplate,
                InfoView.InfoViewType.Packet => PacketTemplate,
                _ => null
            };
            if (template is not null) {
                return template;
            }
        }
        return base.SelectTemplate(obj, container);
    }
}
