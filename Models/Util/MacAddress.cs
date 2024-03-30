using System.Net.NetworkInformation;
using System.Text;

namespace Models.Util;

public class MacAddress(byte[] address) : PhysicalAddress(address) {

    public MacAddress(PhysicalAddress address) : this(address.GetAddressBytes()) {
    }

    public override string ToString() {
        StringBuilder builder = new(base.ToString().ToLower());
        for (int i = 0, j = 2; i < 5; i++, j += 3) {
            builder.Insert(j, '-');
        }
        return builder.ToString();
    }
}
