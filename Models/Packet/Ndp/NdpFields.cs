using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Packet.Ndp;

public struct NdpFields {

    #region NeighborAdvertisement 邻居通告

    public static int NeighborAdvertisementFlagsOffset = 4;

    public static int NeighborAdvertisementOptionsOffset = 24;

    public static int NeighborAdvertisementTargetAddressOffset = 8;

    public static int NeighborSolicitationOptionsAddressOffset = 24;

    public static int NeighborSolicitationTargetAddressOffset = 8;

    #endregion

    #region RouterAdvertisement 路由通告

    public static int RouterAdvertisementCurrentHopLimitOffset = 4;

    public static int RouterAdvertisementExtOffset = 5;

    public static int RouterAdvertisementOptionsOffset = 16;

    public static int RouterAdvertisementReachableTimeOffset = 8;

    public static int RouterAdvertisementRetransmitTimerOffset = 12;

    public static int RouterAdvertisementRouterLifetimeOffset = 6;

    #endregion

    #region RouterSolicitation 路由请求

    public static int RouterSolicitationOptionsOffset = 8;

    #endregion

    #region RedirectMessage 重定向

    public static int RedirectMessageTargetAddressOffset = 8;

    public static int RedirectMessageDestinationAddressOffset = 24;

    public static int RedirectMessageOptionsOffset = 40;

    #endregion
}

public enum OptionTypes : byte {
    SourceLinkLayerAddress = 1,
    TargetLinkLayerAddress = 1,
    PrefixInformation = 2,
    RedirectedHeader = 3,
    Mtu = 4
}
