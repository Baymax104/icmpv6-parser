using System.Net.NetworkInformation;
using System.Text;

namespace Models.Unit;

internal class MacAddress(byte[] address) : PhysicalAddress(address) {

    public override string ToString() {
        StringBuilder builder = new(base.ToString().ToLower());
        for (int i = 0, j = 2; i < 5; i++, j += 3) {
            builder.Insert(j, '-');
        }
        return builder.ToString();
    }

}