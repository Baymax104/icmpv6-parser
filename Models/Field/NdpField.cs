namespace Models.Field;

public struct NdpField {

    #region NeighborAdvertisement 邻居通告

    public const int NAFlagsPosition = 0;

    public const int NAOptionsPosition = 20;

    public const int NATargetAddressPosition = 4;

    public const int NAHeaderLength = 20;

    #endregion

    #region NeighborSolicitation 邻居请求

    public const int NSOptionsPosition = 20;

    public const int NSTargetAddressPosition = 4;

    public const int NSHeaderLength = 20;

    #endregion

    #region RouterAdvertisement 路由通告

    public const int RACurHopLimitPosition = 0;

    public const int RAMOPosition = 1;

    public const int RAOptionsPosition = 12;

    public const int RAReachableTimePosition = 4;

    public const int RAReachableTimeLength = 4;

    public const int RARetransmitTimerPosition = 8;

    public const int RARetransmitTimerLength = 4;

    public const int RARouterLifetimePosition = 2;

    public const int RARouterLifetimeLength = 2;

    public const int RAHeaderLength = 12;

    #endregion

    #region RouterSolicitation 路由请求

    public const int RSOptionsPosition = 4;

    public const int RSHeaderLength = 4;

    #endregion

    #region RedirectMessage 重定向

    public const int RMTargetAddressPosition = 4;

    public const int RMDestinationAddressPosition = 20;

    public const int RMOptionsPosition = 36;

    #endregion
}
